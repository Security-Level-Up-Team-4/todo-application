import { createContext, useContext, useReducer, useEffect, type ReactNode } from 'react';
import { authApi } from '../api/auth';
import type { 
  AuthState, 
  AuthContextType, 
  LoginRequest, 
  SignUpRequest,
  Verify2FARequest,
  Confirm2FASetupRequest 
} from '../models/auth';
import { UserRoles } from '../models/user';

type AuthAction =
  | { type: 'SET_LOADING'; payload: boolean }
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  | { type: 'SET_USER'; payload: { user: any; token: string } }
  | { type: 'LOGOUT' }
  | { type: 'CLEAR_ERROR' };

const initialState: AuthState = {
  user: null,
  token: sessionStorage.getItem('token'),
  isLoading: false,
  isAuthenticated: false,
};

function authReducer(state: AuthState, action: AuthAction): AuthState {
  switch (action.type) {
    case 'SET_LOADING':
      return { ...state, isLoading: action.payload };
    case 'SET_USER':
      return {
        ...state,
        user: action.payload.user,
        token: action.payload.token,
        isAuthenticated: true,
        isLoading: false,
      };
    case 'LOGOUT':
      return {
        ...state,
        user: null,
        token: null,
        isAuthenticated: false,
        isLoading: false,
      };
    case 'CLEAR_ERROR':
      return { ...state };
    default:
      return state;
  }
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [state, dispatch] = useReducer(authReducer, initialState);

  useEffect(() => {
    const token = sessionStorage.getItem('token');
    if (token) {
      dispatch({ type: 'SET_USER', payload: { user: { id: '1', username: 'user', email: 'user@example.com', is2faEnabled: false }, token } });
    }
  }, []);

  const login = async (credentials: LoginRequest) => {
    dispatch({ type: 'SET_LOADING', payload: true });
    try {
      const response = await authApi.login(credentials);
      
      dispatch({ type: 'SET_LOADING', payload: false });
      
      return response;
    } catch (error) {
      dispatch({ type: 'SET_LOADING', payload: false });
      throw error;
    }
  };

  const signup = async (userData: SignUpRequest) => {
    dispatch({ type: 'SET_LOADING', payload: true });
    try {
      const response = await authApi.signup(userData);
      dispatch({ type: 'SET_LOADING', payload: false });
      return response;
    } catch (error) {
      dispatch({ type: 'SET_LOADING', payload: false });
      throw error;
    }
  };

  const verify2FA = async (request: Verify2FARequest, navigate: (path: string) => void) => {
    dispatch({ type: 'SET_LOADING', payload: true });
    try {
      const response = await authApi.verify2FA(request);
      sessionStorage.setItem('token', response.token);
      sessionStorage.setItem('user-role', response.userRoleName);
      sessionStorage.setItem('username', response.userName);
      if (navigate) {
        if (response.userRoleName === UserRoles.ADMIN) {
          navigate('/admin');
        } else {
          navigate('/teams');
        }
      }
      const user = { username: response.userName, role: response.userRoleName, is2faEnabled: true };
      dispatch({ type: 'SET_USER', payload: { user, token: response.token } });
    } catch (error) {
      dispatch({ type: 'SET_LOADING', payload: false });
      throw error;
    }
  };

  const confirm2FASetup = async (request: Confirm2FASetupRequest) => {
    // eslint-disable-next-line no-useless-catch
    try {
      await authApi.confirm2FASetup(request);
      if (state.user) {
        const updatedUser = { ...state.user, is2faEnabled: true };
        dispatch({ type: 'SET_USER', payload: { user: updatedUser, token: state.token! } });
      }
    } catch (error) {
      throw error;
    }
  };

  const logout = () => {
    sessionStorage.removeItem('token');
    dispatch({ type: 'LOGOUT' });
  };

  const value: AuthContextType = {
    ...state,
    login,
    signup,
    verify2FA,
    confirm2FASetup,
    logout,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

// eslint-disable-next-line react-refresh/only-export-components
export function useAuth() {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
}