import { Routes, Route } from 'react-router-dom';
import TaskGrid from './components/TaskGrid';
import TaskDetails from './components/TaskDetails';

function App() {
  return (
    <Routes>
      <Route path="/tasks" element={<TaskGrid />} />
      <Route path="/tasks/:id" element={<TaskDetails />} />
    </Routes>
  );
}

export default App;
