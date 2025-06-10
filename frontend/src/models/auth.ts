export interface User {
  username: string;
  userRoleName: string;
  is2faEnabled: boolean;
}

export interface SignUpRequest {
  username: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface SignUpResponse {
  message: string;
  username: string;
  totpSetupUri: string;
  tempSessionToken: string;
}


export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token?: string;
  requires2FA?: boolean;
  tempSessionToken?: string;
}

export interface Verify2FARequest {
  tempSessionToken: string;
  totpCode: string;
}

export interface Verify2FAResponse {
  token: string;
  userName: string;
  userRoleName: string
}

export interface Confirm2FASetupRequest {
  tempSessionUserId: string;
  token: string;
}

export interface AuthState {
  user: User | null;
  token: string | null;
  isLoading: boolean;
  isAuthenticated: boolean;
}

export interface AuthContextType extends AuthState {
  login: (credentials: LoginRequest) => Promise<LoginResponse>;
  signup: (userData: SignUpRequest) => Promise<SignUpResponse>;
  verify2FA: (request: Verify2FARequest, navigate: (path: string) => void) => Promise<void>;
  confirm2FASetup: (request: Confirm2FASetupRequest) => Promise<void>;
  logout: () => void;
}