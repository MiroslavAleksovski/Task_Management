import { configureStore } from '@reduxjs/toolkit';
import TaskDetailsSlice from './slices/taskDetailsSlice';

export const store = configureStore({
  reducer: {
    TaskDetails: TaskDetailsSlice,
  },
});
