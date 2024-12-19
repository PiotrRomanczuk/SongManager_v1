import React, { createContext, useContext, useState, useEffect } from 'react';
import { authApi } from '../lib/api';
import { AxiosError } from 'axios';

interface AuthContextType {
  token: string | null;
  user: User | null;
  login: (email: string, password: string) => Promise<void>;
  register: (name: string, email: string, password: string) => Promise<void>;
  logout: () => void;
  isAuthenticated: boolean;
  isAdmin: boolean;
}

interface User {
  email: string;
  name: string;
  roles: string[];
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function AuthProvider({ children }: { children: React.ReactNode }) {
  const [token, setToken] = useState<string | null>(localStorage.getItem('token'));
  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  const login = async (email: string, password: string) => {
    try {
      const data = await authApi.login(email, password);
      setToken(data.token);
      const userData = { 
        email: data.email, 
        name: data.name, // Update here
        roles: data.roles 
      };
      setUser(userData);
      localStorage.setItem('token', data.token);
      localStorage.setItem('user', JSON.stringify(userData));
    } catch (error) {
      if (error instanceof AxiosError && error.response?.data) {
        throw new Error(error.response.data);
      }
      throw error;
    }
  };

  const register = async (name: string, email: string, password: string) => {
    try {
      const data = await authApi.register(name, email, password);
      setToken(data.token);
      const userData = { 
        email: data.email, 
        name: data.name, // Update here
        roles: data.roles 
      };
      setUser(userData);
      localStorage.setItem('token', data.token);
      localStorage.setItem('user', JSON.stringify(userData));
    } catch (error) {
      if (error instanceof AxiosError && error.response?.data) {
        throw new Error(error.response.data);
      }
      throw error;
    }
  };

  const logout = () => {
    setToken(null);
    setUser(null);
    localStorage.removeItem('token');
    localStorage.removeItem('user');
  };

  const value = {
    token,
    user,
    login,
    register,
    logout,
    isAuthenticated: !!token,
    isAdmin: user?.roles?.includes('Admin') || false
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
}
