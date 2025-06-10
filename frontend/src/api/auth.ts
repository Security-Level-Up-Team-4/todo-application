import { apiBaseUrl } from "../config";
class ApiError extends Error {
  constructor(public status: number, message: string) {
    super(message);
    this.name = 'ApiError';
  }
}

async function apiRequest<T>(
  endpoint: string,
  options: RequestInit = {}
): Promise<T> {
  const url = `${apiBaseUrl}/api${endpoint}`;
  const token = sessionStorage.getItem('token');
  
  const config: RequestInit = {
    headers: {
      'Content-Type': 'application/json',
      ...(token && { Authorization: `Bearer ${token}` }),
      ...options.headers,
    },
    ...options,
  };

  try {
    const response = await fetch(url, config);
    
    if (!response.ok) {
      const errorData = await response.json().catch(() => ({}));
      throw new ApiError(
        response.status, 
        errorData.message || `HTTP error! status: ${response.status}`
      );
    }

    return await response.json();
  } catch (error) {
    if (error instanceof ApiError) {
      throw error;
    }
    throw new ApiError(0, 'Network error occurred');
  }
}

export const authApi = {
  signup: async (userData: import('../models/auth').SignUpRequest) => {
    return apiRequest<import('../models/auth').SignUpResponse>('/auth/signup', {
      method: 'POST',
      body: JSON.stringify(userData),
    });
  },

  login: async (credentials: import('../models/auth').LoginRequest) => {
    return apiRequest<import('../models/auth').LoginResponse>('/auth/login', {
      method: 'POST',
      body: JSON.stringify(credentials),
    });
  },

  verify2FA: async (request: import('../models/auth').Verify2FARequest) => {
    return apiRequest<import('../models/auth').Verify2FAResponse>('/auth/verify-2fa', {
      method: 'POST',
      body: JSON.stringify(request),
    });
  },

  confirm2FASetup: async (request: import('../models/auth').Confirm2FASetupRequest) => {
    return apiRequest<{ message: string }>('/auth/confirm-2fa', {
      method: 'POST',
      body: JSON.stringify(request),
    });
  },
};

export { ApiError };