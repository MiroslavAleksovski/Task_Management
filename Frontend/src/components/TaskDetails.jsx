import { useEffect, useState } from 'react';
import { useParams, useNavigate, useSearchParams } from 'react-router-dom';
import { Container, TextField, Button, Box, Typography, Paper, Checkbox, FormControlLabel } from '@mui/material';
import settings from '../../appsettings.json';

function TaskDetails() {
  const { id } = useParams();
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [task, setTask] = useState({ name: '', description: '', isCompleted: false });
  const [loading, setLoading] = useState(id !== 'new');
  const baseURL = settings.baseURL;
  const isNewTask = id === 'new';
  const isViewMode = searchParams.get('mode') === 'view';

  // Promise-based helper using callbacks (no async/await)
  function fetchWithCallbacks(url, options, { onSuccess, onError } = {}) {
    return fetch(url, options)
      .then(response => {
        return response.json();
      })
      .then(data => {
        if (onSuccess) onSuccess(data);
        return data;
      })
      .catch(err => {
        if (onError) onError(err);
        throw err;
      });
  }

  useEffect(() => {
    if (!isNewTask) {
      function loadTaskDetail() {
        setLoading(true);

        fetch(`${baseURL}task/gettask/${id}`,
          { method: 'GET', headers: { 'Content-Type': 'application/json' } })
          .then(response => {
            response.json().then(data => {
              setTask(data);
            });
          })
          .catch(err => {
          })
          .finally(() => setLoading(false));
      }

      loadTaskDetail();
    }
  }, [id, baseURL, isNewTask]);

  const handleInputChange = (e) => {
    const { name, value, type, checked } = e.target;
    setTask((prevTask) => ({
      ...prevTask,
      [name]: type === 'checkbox' ? checked : value,
    }));
  };

  const handleSave = () => {
    const addEditTask = {
      id: task.id || null,
      name: task.name.trim(),
      description: task.description || null,
      isCompleted: !!task.isCompleted,
    };
    setLoading(true);

    fetchWithCallbacks(
      `${baseURL}task/AddUpdateTask`,
      {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(addEditTask),
      },
      {
        onSuccess: () => navigate('/tasks'),
        onError: err => {
          console.warn('Error saving task:', err);
          alert('Unable to save task. Please try again.');
        },
      }
    )
      .catch(() => { })
      .finally(() => setLoading(false));
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
        {isNewTask ? 'Add New Task' : isViewMode ? 'View Task' : 'Task Details'}
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
            disabled={isViewMode}
          />

          <FormControlLabel
            control={
              <Checkbox
                name="isCompleted"
                checked={!!task.isCompleted}
                onChange={handleInputChange}
                disabled={isViewMode}
              />
            }
            label="Completed"
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
            disabled={isViewMode}
          />
        </Box>
      </Paper>

      <Box sx={{ display: 'flex', gap: 2 }}>
        {!isViewMode && (
          <Button
            variant="contained"
            color="primary"
            onClick={handleSave}
          >
            {isNewTask ? 'Create Task' : 'Save Changes'}
          </Button>
        )}
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
