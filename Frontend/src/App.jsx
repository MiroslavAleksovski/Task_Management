import { Routes, Route, Navigate } from 'react-router-dom';
import TaskGrid from './components/TaskGrid';
import TaskDetails from './components/TaskDetails';

function App() {
  return (
    <Routes>
      <Route path="/" element={<Navigate to="/tasks" replace />} />
      <Route path="/tasks" element={<TaskGrid />} />
      <Route path="/tasks/new" element={<TaskDetails />} />
      <Route path="/tasks/:id" element={<TaskDetails />} />
      <Route path="*" element={<Navigate to="/tasks" replace />} />
    </Routes>
  );
}

export default App;
