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

function Register() {
  const navigate = useNavigate();
  const baseURL = settings.baseURL;

  const [form, setForm] = useState({
    name: '',
    surname: '',
    email: '',
    password: '',
    confirmPassword: '',
  });

  const [errors, setErrors] = useState({});
  const [apiError, setApiError] = useState('');
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
    setErrors((prev) => ({ ...prev, [name]: '' }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setApiError('');

    setLoading(true);

    fetch(`${baseURL}Auth/Register`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({
        name: form.name.trim(),
        surname: form.surname.trim(),
        email: form.email.trim(),
        password: form.password,
        confirmPassword: form.confirmPassword,
      }),
    })
      .then((response) => {
        if (!response.ok) {
          return response.text().then((text) => {
            throw new Error(text || 'Registration failed');
          });
        }
        return response.json();
      })
      .then(() => {
        navigate('/login');
      })
      .catch((err) => {
        setApiError(err.message || 'Registration failed. Please try again.');
      })
      .finally(() => setLoading(false));
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 8 }}>
      <Paper elevation={3} sx={{ p: 4, borderRadius: 3 }}>
        <Typography variant="h4" fontWeight={700} textAlign="center" mb={1}>
          Create Account
        </Typography>
        <Typography variant="body2" color="text.secondary" textAlign="center" mb={3}>
          Fill in the details below to register
        </Typography>

        {apiError && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {apiError}
          </Alert>
        )}

        <Box component="form" onSubmit={handleSubmit} noValidate>
          <Box sx={{ display: 'flex', gap: 2, mb: 2 }}>
            <TextField
              label="Name"
              name="name"
              value={form.name}
              onChange={handleChange}
              error={!!errors.name}
              helperText={errors.name}
              fullWidth
              required
            />
            <TextField
              label="Surname"
              name="surname"
              value={form.surname}
              onChange={handleChange}
              error={!!errors.surname}
              helperText={errors.surname}
              fullWidth
              required
            />
          </Box>

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
            sx={{ mb: 2 }}
          />

          <TextField
            label="Confirm Password"
            name="confirmPassword"
            type="password"
            value={form.confirmPassword}
            onChange={handleChange}
            error={!!errors.confirmPassword}
            helperText={errors.confirmPassword}
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
            {loading ? 'Registering...' : 'Register'}
          </Button>

          <Typography variant="body2" textAlign="center" mt={2}>
            Already have an account?{' '}
            <Button
              variant="text"
              size="small"
              onClick={() => navigate('/login')}
              sx={{ textTransform: 'none' }}
            >
              Login
            </Button>
          </Typography>
        </Box>
      </Paper>
    </Container>
  );
}

export default Register;
