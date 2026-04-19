export interface ProjectDto {
  id: string;
  name: string;
  description: string;
  startDate: string;
  endDate: string | null;
  createdAt: string;
  taskCount: number;
}

export interface ProjectCreateDto {
  name: string;
  description: string;
  startDate: string;
  endDate: string | null;
}

export interface ProjectUpdateDto {
  name: string;
  description: string;
  startDate: string;
  endDate: string | null;
}
