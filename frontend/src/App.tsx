import { Routes, Route } from "react-router-dom";
import Teams from "./pages/Teams";
import Todos from "./pages/Todos";
import AdminManagement from "./pages/AdminManagement";
import Timeline from "./pages/Timeline";
import PageNotFound from "./pages/PageNotFound";
import Todo from "./pages/Todo";
import { AuthProvider, useAuth } from './contexts/AuthContext';
import LoginForm from './pages/LoginForm';
import SignupForm from './pages/SignupForm';
import TwoFactorSetup from './pages/TwoFactorSetup';
import TwoFactorVerification from './pages/TwoFactorVerification';
import { useState } from "react";
import { UserRoles } from "./models/user";

type AuthStep = 'login' | 'signup' | '2fa-setup' | '2fa-verify';

function AuthFlow() {
  const { isAuthenticated } = useAuth();
  const [currentStep, setCurrentStep] = useState<AuthStep>('login');
  const [tempSessionToken, setTempSessionToken] = useState('');
  const [totpSetupUri, setTotpSetupUri] = useState('');

  if (isAuthenticated) {
  const userRole = sessionStorage.getItem('user-role');

  if (userRole ===  UserRoles.ADMIN) {
    return (
      <Routes>
        <Route path="/" element={<AdminManagement />} />
        <Route path="/admin" element={<AdminManagement />} />
        <Route path="*" element={<PageNotFound />} />
      </Routes>
    );
  }

  return (
    <Routes>
      <Route path="/" element={<Teams />} />
      <Route path="/teams" element={<Teams />} />
      <Route path="/todos" element={<Todos />} />
      <Route path="/todo" element={<Todo />} />
      <Route path="/todo/timeline" element={<Timeline />} />
      <Route path="*" element={<PageNotFound />} />
    </Routes>
  );
  }

  const handleSignupSuccess = (setupUri: string, tempToken: string) => {
    setTotpSetupUri(setupUri);
    setTempSessionToken(tempToken);
    setCurrentStep('2fa-setup');
  };

  const handleRequires2FA = (tempToken: string) => {
    setTempSessionToken(tempToken);
    setCurrentStep('2fa-verify');
  };

  const handle2FASetupComplete = () => {
    setCurrentStep('login');
    setTempSessionToken('')
    setTotpSetupUri('');
  };

  const renderAuthStep = () => {
    switch (currentStep) {
      case 'login':
        return (
          <LoginForm
            onSignupClick={() => setCurrentStep('signup')}
            onRequires2FA={handleRequires2FA}
          />
        );
      case 'signup':
        return (
          <SignupForm
            onLoginClick={() => setCurrentStep('login')}
            onSuccess={handleSignupSuccess}
          />
        );
      case '2fa-setup':
        return (
          <TwoFactorSetup
            totpSetupUri={totpSetupUri}
            tempSessionToken={tempSessionToken}
            onComplete={handle2FASetupComplete}
          />
        );
      case '2fa-verify':
        return (
          <TwoFactorVerification
            tempSessionToken={tempSessionToken}
            onBack={() => setCurrentStep('login')}
          />
        );
      default:
        return null;
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 via-white to-purple-50 flex items-center justify-center p-4">
      <div className="bg-white rounded-2xl shadow-xl p-8 border border-gray-100">
        {renderAuthStep()}
      </div>
    </div>
  );
}

function App() {
  return (
    <AuthProvider>
      <AuthFlow />
    </AuthProvider>
  );
}

export default App;
