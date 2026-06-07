import { createSlice } from '@reduxjs/toolkit';

const TaskDetailsSlice = createSlice({
  name: 'TaskDetails',
  initialState: {
    mode: 'edit', // 'view' or 'edit'
  },
  reducers: {
    setViewMode: (state, action) => {
      state.mode = action.payload;
    },
    resetMode: (state) => {
      state.mode = 'edit';
    },
  },
});

export const { setViewMode, resetMode } = TaskDetailsSlice.actions;
export default TaskDetailsSlice.reducer;
