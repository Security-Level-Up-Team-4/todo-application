import React, { useState } from 'react';
import { Shield } from 'lucide-react';
import Button from '../components/Button';
import Input from '../components/Input';
import { useAuth } from '../contexts/AuthContext';
import { ApiError } from '../api/auth';

interface TwoFactorVerificationProps {
  tempSessionToken: string;
  onBack: () => void;
}

export default function TwoFactorVerification({ tempSessionToken, onBack }: TwoFactorVerificationProps) {
  const { verify2FA, isLoading } = useAuth();
  const [totpCode, setTotpCode] = useState('');
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (!totpCode.trim()) {
      setError('Please enter the verification code');
      return;
    }

    if (totpCode.length !== 6) {
      setError('Verification code must be 6 digits');
      return;
    }

    try {
      await verify2FA({
        tempSessionToken,
        totpCode,
      });
    } catch (error) {
      if (error instanceof ApiError) {
        setError(error.message);
      } else {
        setError('Invalid verification code');
      }
    }
  };

  return (
    <div className="w-full max-w-md space-y-6">
      <div className="text-center">
        <div className="mx-auto w-16 h-16 bg-blue-100 rounded-full flex items-center justify-center mb-4">
          <Shield className="w-8 h-8 text-blue-600" />
        </div>
        <h2 className="text-3xl font-bold text-gray-900">Two-Factor Authentication</h2>
        <p className="mt-2 text-gray-600">
          Enter the 6-digit code from your authenticator app
        </p>
      </div>

      <form onSubmit={handleSubmit} className="space-y-4">
        <Input
          type="text"
          label="Verification Code"
          placeholder="000000"
          value={totpCode}
          onChange={(e) => setTotpCode(e.target.value.replace(/\D/g, '').slice(0, 6))}
          error={error}
          maxLength={6}
          className="text-center text-lg font-mono tracking-widest"
        />

        <Button
          type="submit"
          loading={isLoading}
          className="w-full"
          size="lg"
        >
          Verify Code
        </Button>

        <Button
          type="button"
          variant="outline"
          onClick={onBack}
          className="w-full"
        >
          Back to Login
        </Button>
      </form>

      <div className="text-center">
        <p className="text-sm text-gray-600">
          Can't access your authenticator app?{' '}
          <button className="text-blue-600 hover:text-blue-700 font-medium">
            Get help
          </button>
        </p>
      </div>
    </div>
  );
}