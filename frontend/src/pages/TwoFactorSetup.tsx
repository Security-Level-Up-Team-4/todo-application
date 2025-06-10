import React, { useState, useEffect } from 'react';
import { Shield, Copy, Check } from 'lucide-react';
import QRCode from 'qrcode';
import Button from '../components/Button';
import Input from '../components/Input';
import { useAuth } from '../contexts/AuthContext';
import { ApiError } from '../api/auth';

interface TwoFactorSetupProps {
  totpSetupUri: string;
  tempSessionToken: string;
  onComplete: () => void;
}

export default function TwoFactorSetup({ totpSetupUri, tempSessionToken, onComplete }: TwoFactorSetupProps) {
  const { confirm2FASetup } = useAuth();
  const [qrCodeUrl, setQrCodeUrl] = useState('');
  const [totpCode, setTotpCode] = useState('');
  const [error, setError] = useState('');
  const [isVerifying, setIsVerifying] = useState(false);
  const [copied, setCopied] = useState(false);

  const secret = totpSetupUri.match(/secret=([^&]*)/)?.[1] || '';

  useEffect(() => {
    const generateQRCode = async () => {
      try {
        const url = await QRCode.toDataURL(totpSetupUri);
        setQrCodeUrl(url);
      } catch (error) {
        console.error('Error generating QR code:', error);
      }
    };

    generateQRCode();
  }, [totpSetupUri]);

  const handleCopySecret = async () => {
    try {
      await navigator.clipboard.writeText(secret);
      setCopied(true);
      setTimeout(() => setCopied(false), 2000);
    } catch (error) {
      console.error('Failed to copy secret:', error);
    }
  };

  const handleVerify = async (e: React.FormEvent) => {
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

    setIsVerifying(true);

    try {
      await confirm2FASetup({
        tempSessionUserId: tempSessionToken, 
        token: totpCode,
      });
      onComplete();
    } catch (error) {
      if (error instanceof ApiError) {
        setError(error.message);
      } else {
        setError('Invalid verification code');
      }
    } finally {
      setIsVerifying(false);
    }
  };

  return (
    <div className="w-full max-w-md space-y-6">
      <div className="text-center">
        <div className="mx-auto w-16 h-16 bg-blue-100 rounded-full flex items-center justify-center mb-4">
          <Shield className="w-8 h-8 text-blue-600" />
        </div>
        <h2 className="text-3xl font-bold text-gray-900">Set up 2FA</h2>
        <p className="mt-2 text-gray-600">
          Secure your account with two-factor authentication
        </p>
      </div>

      <div className="space-y-4">
        <div className="text-center">
          <h3 className="text-lg font-medium text-gray-900 mb-3">
            Scan QR Code
          </h3>
          {qrCodeUrl ? (
            <div className="mx-auto w-48 h-48 bg-white p-4 rounded-lg border-2 border-gray-200">
              <img src={qrCodeUrl} alt="QR Code" className="w-full h-full" />
            </div>
          ) : (
            <div className="mx-auto w-48 h-48 bg-gray-100 rounded-lg flex items-center justify-center">
              <div className="animate-spin rounded-full h-8 w-8 border-b-2 border-blue-600"></div>
            </div>
          )}
          <p className="mt-2 text-sm text-gray-600">
            Use your authenticator app to scan this QR code
          </p>
        </div>

        <div className="text-center">
          <h4 className="text-sm font-medium text-gray-700 mb-2">
            Or enter this key manually:
          </h4>
          <div className="flex items-center space-x-2 p-3 bg-gray-50 rounded-lg">
            <code className="flex-1 text-sm font-mono text-gray-800 break-all">
              {secret}
            </code>
            <button
              onClick={handleCopySecret}
              className="p-1 text-gray-500 hover:text-gray-700 transition-colors cursor-pointer"
            >
              {copied ? (
                <Check size={16} className="text-green-600" />
              ) : (
                <Copy size={16} />
              )}
            </button>
          </div>
        </div>

        <form onSubmit={handleVerify} className="space-y-4">
          <Input
            type="text"
            label="Verification Code"
            placeholder="Enter 6-digit code"
            value={totpCode}
            onChange={(e) => setTotpCode(e.target.value.replace(/\D/g, '').slice(0, 6))}
            error={error}
            maxLength={6}
            className="text-center text-lg font-mono tracking-widest"
          />

          <Button
            type="submit"
            loading={isVerifying}
            className="w-full cursor-pointer"
            size="lg"
          >
            Verify & Complete Setup
          </Button>
        </form>
      </div>

      <div className="text-xs text-gray-500 text-center space-y-1">
        <p>Popular authenticator apps:</p>
        <p>Google Authenticator, Microsoft Authenticator</p>
      </div>
    </div>
  );
}