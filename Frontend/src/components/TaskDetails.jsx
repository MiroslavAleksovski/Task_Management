import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, TextField, Button, Box, Typography, Paper } from '@mui/material';
import settings from '../../appsettings.json';

function TaskDetails() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [task, setTask] = useState({ name: '', description: '' });
  const [loading, setLoading] = useState(id !== 'new');
  const baseURL = settings.baseURL;
  const isNewTask = id === 'new';

  useEffect(() => {
    if (!isNewTask) {
      async function loadTaskDetail() {
        try {
          const response = await fetch(`${baseURL}task/gettask/${id}`);
          if (!response.ok) {
            console.warn('Failed to load task details');
            setLoading(false);
            return;
          }

          const data = await response.json();
          setTask(data);
        } catch (error) {
          console.warn('Could not load task details from backend:', error);
        } finally {
          setLoading(false);
        }
      }

      loadTaskDetail();
    }
  }, [id, baseURL, isNewTask]);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setTask((prevTask) => ({
      ...prevTask,
      [name]: value,
    }));
  };

  const handleSave = async () => {
    const addEditTask = {
      id: task.id || null,
      name: task.name.trim(),
      description: task.description || null,
    };

    setLoading(true);

    try {
      const response = await fetch(`${baseURL}task/AddUpdateTask`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(addEditTask),
      });

      if (!response.ok) {
        console.warn('Failed to save task');
        alert('Unable to save task. Please try again.');
        return;
      }

      navigate('/tasks');
    } catch (error) {
      console.warn('Error saving task:', error);
      alert('Unable to save task. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <Container sx={{ py: 4 }}>
      <Typography>Loading...</Typography>
    </Container>;
  }

  if (!isNewTask && !task) {
    return (
      <Container sx={{ py: 4 }}>
        <Typography variant="h4" sx={{ mb: 2 }}>Task Not Found</Typography>
        <Button variant="contained" onClick={() => navigate('/tasks')}>
          Back to Tasks
        </Button>
      </Container>
    );
  }

  return (
    <Container sx={{ py: 4 }}>
      <Typography variant="h4" sx={{ mb: 3 }}>
        {isNewTask ? 'Add New Task' : 'Task Details'}
      </Typography>

      <Paper sx={{ p: 3, mb: 3 }}>
        <Box sx={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
          <TextField
            label="Task Name"
            name="name"
            value={task.name}
            onChange={handleInputChange}
            placeholder="Enter task name"
            fullWidth
            variant="outlined"
          />

          <TextField
            label="Description"
            name="description"
            value={task.description || ''}
            onChange={handleInputChange}
            placeholder="Enter task description"
            multiline
            rows={4}
            fullWidth
            variant="outlined"
          />

          {task.id && (
            <Typography variant="body2">
              <strong>ID:</strong> {task.id}
            </Typography>
          )}
        </Box>
      </Paper>

      <Box sx={{ display: 'flex', gap: 2 }}>
        <Button 
          variant="contained" 
          color="primary"
          onClick={handleSave}
        >
          {isNewTask ? 'Create Task' : 'Save Changes'}
        </Button>
        <Button 
          variant="outlined"
          onClick={() => navigate('/tasks')}
        >
          Back to Tasks
        </Button>
      </Box>
    </Container>
  );
}

export default TaskDetails;
