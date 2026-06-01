import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Container,
  Button,
  Box,
  Typography,
  IconButton,
  Paper,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Checkbox,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import EditIcon from '@mui/icons-material/Edit';
import VisibilityIcon from '@mui/icons-material/Visibility';
import DeleteIcon from '@mui/icons-material/Delete';
import settings from '../../appsettings.json';

function TaskGrid() {
  const [tasks, setTasks] = useState([]);
  const [deleteDialogOpen, setDeleteDialogOpen] = useState(false);
  const [taskToDelete, setTaskToDelete] = useState(null);
  const [filterIsCompleted, setFilterIsCompleted] = useState('all');
  const baseURL = settings.baseURL;
  const navigate = useNavigate();

  const loadTasks = async () => {
    try {
      let isCompletedValue = null;
      if (filterIsCompleted === 'true') isCompletedValue = true;
      if (filterIsCompleted === 'false') isCompletedValue = false;

      const response = await fetch(`${baseURL}task/gettasks`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(isCompletedValue === null ? null : { isCompleted: isCompletedValue }),
      });
      if (!response.ok) {
        console.warn('Backend responded with status', response.status);
        return;
      }

      const data = await response.json();
      if (Array.isArray(data)) {
        setTasks(data);
      }
    } catch (error) {
      console.warn('Could not load tasks from backend:', error);
    }
  };

  useEffect(() => {
    loadTasks();
  }, [baseURL, filterIsCompleted]);

  const handleEditClick = (taskId) => {
    navigate(`/tasks/${taskId}`);
  };

  const handleViewClick = (taskId) => {
    navigate(`/tasks/${taskId}?mode=view`);
  };

  const handleDeleteClick = (task) => {
    setTaskToDelete(task);
    setDeleteDialogOpen(true);
  };

  const handleCancelDelete = () => {
    setTaskToDelete(null);
    setDeleteDialogOpen(false);
  };

  const handleConfirmDelete = async () => {
    if (!taskToDelete?.id) {
      return;
    }

    try {
      const response = await fetch(`${baseURL}task/DeleteTask/${taskToDelete.id}`, {
        method: 'DELETE',
      });

      if (!response.ok) {
        console.warn('Failed to delete task', response.status);
        alert('Unable to delete task. Please try again.');
        return;
      }

      handleCancelDelete();
      loadTasks();
    } catch (error) {
      console.warn('Error deleting task:', error);
      alert('Unable to delete task. Please try again.');
    }
  };

  const handleAddNewTask = () => {
    navigate('/tasks/new');
  };

  const handleFilterChange = (e) => {
    setFilterIsCompleted(e.target.value);
  };

  return (
    <Container sx={{ py: 4 }}>
      <Typography variant="h4" sx={{ mb: 3 }}>
        Task Management
      </Typography>

      <Box sx={{ mb: 3, display: 'flex', gap: 2, alignItems: 'center' }}>
        <Button 
          variant="contained" 
          color="primary"
          startIcon={<AddIcon />}
          onClick={handleAddNewTask}
        >
          Add New Task
        </Button>

        <FormControl sx={{ minWidth: 160 }}>
          <InputLabel id="filter-label">Filter by Status</InputLabel>
          <Select
            labelId="filter-label"
            value={filterIsCompleted}
            label="Filter by Status"
            onChange={handleFilterChange}
          >
            <MenuItem value="all">All Tasks</MenuItem>
            <MenuItem value="true">Completed</MenuItem>
            <MenuItem value="false">Incomplete</MenuItem>
          </Select>
        </FormControl>
      </Box>

      <Paper>
        <TableContainer>
          <Table sx={{ minWidth: 650 }} aria-label="tasks table">
            <TableHead>
              <TableRow>
                <TableCell>Task</TableCell>
                <TableCell align="center">Completed</TableCell>
                <TableCell align="right">Actions</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {tasks.map((task) => (
                <TableRow key={task.id} hover>
                  <TableCell component="th" scope="row">
                    {task.name}
                  </TableCell>
                  <TableCell align="center">
                    <Checkbox checked={!!task.isCompleted} disabled />
                  </TableCell>
                  <TableCell align="right">
                    <IconButton
                      aria-label={`view ${task.name}`}
                      onClick={() => handleViewClick(task.id)}
                    >
                      <VisibilityIcon />
                    </IconButton>
                    <IconButton
                      aria-label={`edit ${task.name}`}
                      onClick={() => handleEditClick(task.id)}
                    >
                      <EditIcon />
                    </IconButton>
                    <IconButton
                      aria-label={`delete ${task.name}`}
                      onClick={() => handleDeleteClick(task)}
                    >
                      <DeleteIcon />
                    </IconButton>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Paper>

      <Dialog open={deleteDialogOpen} onClose={handleCancelDelete}>
        <DialogTitle>Confirm Delete</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Are you sure you want to delete the task "{taskToDelete?.name}"? This action cannot be undone.
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCancelDelete}>Cancel</Button>
          <Button color="error" onClick={handleConfirmDelete}>
            Delete
          </Button>
        </DialogActions>
      </Dialog>
    </Container>
  );
}

export default TaskGrid;
