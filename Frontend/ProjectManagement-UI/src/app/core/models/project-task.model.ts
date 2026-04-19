export enum ProjectTaskStatus {
  Pending = 0,
  InProgress = 1,
  Completed = 2
}

export interface ProjectTaskDto {
  id: string;
  title: string;
  description: string;
  status: ProjectTaskStatus;
  startDate: string;
  dueDate: string;
  projectId: string;
}

export interface ProjectTaskCreateDto {
  title: string;
  description: string;
  startDate: string;
  dueDate: string;
  status: ProjectTaskStatus;
  projectId: string;
}

export interface ProjectTaskUpdateDto {
  title: string;
  description: string;
  startDate: string;
  dueDate: string;
  status: ProjectTaskStatus;
}
