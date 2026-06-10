import { Routes, Route, Navigate } from 'react-router-dom';
import TaskGrid from './components/TaskGrid';
import TaskDetails from './components/TaskDetails';
import Register from './components/Register';
import Login from './components/Login';
import AuthGuard from './components/AuthGuard';

function App() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/tasks" replace />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} />
      <Route path="/tasks" element={<AuthGuard><TaskGrid /></AuthGuard>} />
      <Route path="/tasks/new" element={<AuthGuard><TaskDetails /></AuthGuard>} />
      <Route path="/tasks/:id" element={<AuthGuard><TaskDetails /></AuthGuard>} />
      <Route path="*" element={<Navigate to="/tasks" replace />} />
    </Routes>
  );
}

export default App;
