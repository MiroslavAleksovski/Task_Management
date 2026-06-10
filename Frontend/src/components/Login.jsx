import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Container,
  TextField,
  Button,
  Box,
  Typography,
  Paper,
  Alert,
} from '@mui/material';
import settings from '../../appsettings.json';

function Login() {
  const navigate = useNavigate();
  const baseURL = settings.baseURL;

  const [form, setForm] = useState({
    email: '',
    password: '',
  });

  const [errors, setErrors] = useState({});
  const [apiError, setApiError] = useState('');
  const [loading, setLoading] = useState(false);

  const validate = () => {
    const newErrors = {};
    if (!form.email.trim()) {
      newErrors.email = 'Email is required';
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(form.email)) {
      newErrors.email = 'Enter a valid email address';
    }
    if (!form.password) {
      newErrors.password = 'Password is required';
    }
    return newErrors;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
    setErrors((prev) => ({ ...prev, [name]: '' }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setApiError('');

    const validationErrors = validate();
    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }

    setLoading(true);

    fetch(`${baseURL}Auth/Login`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include',
      body: JSON.stringify({
        email: form.email.trim(),
        password: form.password,
      }),
    })
      .then((response) => {
        if (!response.ok) {
          return response.text().then((text) => {
            throw new Error(text || 'Login failed');
          });
        }
        return response.json();
      })
      .then(() => {
        navigate('/tasks');
      })
      .catch((err) => {
        setApiError(err.message || 'Login failed. Please try again.');
      })
      .finally(() => setLoading(false));
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 8 }}>
      <Paper elevation={3} sx={{ p: 4, borderRadius: 3 }}>
        <Typography variant="h4" fontWeight={700} textAlign="center" mb={1}>
          Welcome Back
        </Typography>
        <Typography variant="body2" color="text.secondary" textAlign="center" mb={3}>
          Sign in to your account
        </Typography>

        {apiError && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {apiError}
          </Alert>
        )}

        <Box component="form" onSubmit={handleSubmit} noValidate>
          <TextField
            label="Email"
            name="email"
            type="email"
            value={form.email}
            onChange={handleChange}
            error={!!errors.email}
            helperText={errors.email}
            fullWidth
            required
            sx={{ mb: 2 }}
          />

          <TextField
            label="Password"
            name="password"
            type="password"
            value={form.password}
            onChange={handleChange}
            error={!!errors.password}
            helperText={errors.password}
            fullWidth
            required
            sx={{ mb: 3 }}
          />

          <Button
            type="submit"
            variant="contained"
            fullWidth
            size="large"
            disabled={loading}
            sx={{ py: 1.5, borderRadius: 2, textTransform: 'none', fontSize: '1rem' }}
          >
            {loading ? 'Signing in...' : 'Login'}
          </Button>

          <Typography variant="body2" textAlign="center" mt={2}>
            Don't have an account?{' '}
            <Button
              variant="text"
              size="small"
              onClick={() => navigate('/register')}
              sx={{ textTransform: 'none' }}
            >
              Register
            </Button>
          </Typography>
        </Box>
      </Paper>
    </Container>
  );
}

export default Login;
