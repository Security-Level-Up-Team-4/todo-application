import React, { useState } from 'react';
import { Mail, Lock, User } from 'lucide-react';
import Button from '../components/Button';
import Input from '../components/Input';
import { useAuth } from '../contexts/AuthContext';
import { ApiError } from '../api/auth';
import type { SignUpRequest } from '../models/auth';

interface SignupFormProps {
  onLoginClick: () => void;
  onSuccess: (totpSetupUri: string, tempSessionToken: string) => void;
}

export default function SignupForm({ onLoginClick, onSuccess }: SignupFormProps) {
  const { signup, isLoading } = useAuth();
  const [formData, setFormData] = useState<SignUpRequest>({
    username: '',
    email: '',
    password: '',
    confirmPassword: '',
  });
  const [errors, setErrors] = useState<Partial<SignUpRequest>>({});
  const [submitError, setSubmitError] = useState('');

  const validateForm = (): boolean => {
    const newErrors: Partial<SignUpRequest> = {};

    if (!formData.username.trim()) {
      newErrors.username = 'Username is required';
    } else if (formData.username.length < 3) {
      newErrors.username = 'Username must be at least 3 characters';
    } else if (formData.username.length > 100) {
      newErrors.username = 'Username cannot be over 100 characters';
    }

    if (!formData.email.trim()) {
      newErrors.email = 'Email is required';
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      newErrors.email = 'Please enter a valid email address';
    }

    if (!formData.password) {
      newErrors.password = 'Password is required';
    } else if (formData.password.length < 8) {
      newErrors.password = 'Password must be at least 8 characters';
    }

    if (!formData.confirmPassword) {
      newErrors.confirmPassword = 'Please confirm your password';
    } else if (formData.password !== formData.confirmPassword) {
      newErrors.confirmPassword = 'Passwords do not match';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitError('');

    if (!validateForm()) return;

    try {
      const response = await signup(formData);
      onSuccess(response.totpSetupUri, response.tempSessionToken);
    } catch (error) {
      if (error instanceof ApiError) {
        setSubmitError(error.message);
      } else {
        setSubmitError('An unexpected error occurred');
      }
    }
  };

  const handleInputChange = (field: keyof SignUpRequest) => (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    setFormData(prev => ({ ...prev, [field]: e.target.value }));
    if (errors[field]) {
      setErrors(prev => ({ ...prev, [field]: undefined }));
    }
  };

  return (
    <div className="w-full max-w-md space-y-6">
      <div className="text-center" style={{ maxWidth: '600px' }}>
        <h2 className="text-3xl font-bold text-gray-900">Create account</h2>
      </div>

      <form onSubmit={handleSubmit} className="space-y-4">
        <Input
          type="text"
          label="Username"
          placeholder="Choose a username"
          value={formData.username}
          onChange={handleInputChange('username')}
          error={errors.username}
          icon={<User size={18} />}
        />

        <Input
          type="email"
          label="Email"
          placeholder="Enter your email"
          value={formData.email}
          onChange={handleInputChange('email')}
          error={errors.email}
          icon={<Mail size={18} />}
        />

        <div className="relative">
          <Input
            type={'password'}
            label="Password"
            placeholder="Create a password"
            value={formData.password}
            onChange={handleInputChange('password')}
            error={errors.password}
            icon={<Lock size={18} />}
          />
        </div>

        <Input
          type={'password'}
          label="Confirm Password"
          placeholder="Confirm your password"
          value={formData.confirmPassword}
          onChange={handleInputChange('confirmPassword')}
          error={errors.confirmPassword}
          icon={<Lock size={18} />}
        />

        {submitError && (
          <div className="p-3 bg-red-50 border border-red-200 rounded-lg text-red-700 text-sm">
            {submitError}
          </div>
        )}

        <Button
          type="submit"
          loading={isLoading}
          className="w-full cursor-pointer"
          size="lg"
        >
          Create Account
        </Button>
      </form>

      <div className="text-center">
        <span className="text-gray-600">Already have an account? </span>
        <button
          onClick={onLoginClick}
          className="text-blue-600 hover:text-blue-700 font-medium transition-colors cursor-pointer"
        >
          Sign in
        </button>
      </div>
    </div>
  );
}