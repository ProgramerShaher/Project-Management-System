import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { ProjectTaskDto, ProjectTaskCreateDto, ProjectTaskUpdateDto, ProjectTaskStatus } from '../models/project-task.model';
import { PagedList } from '../models/paged-list.model';
import { ApiResponse } from '../models/api-response.model';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private api = inject(ApiService);
  private readonly endpoint = '/tasks';

  getTasks(projectId?: string, status?: ProjectTaskStatus, pageNumber = 1, pageSize = 10): Observable<ApiResponse<PagedList<ProjectTaskDto>>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
      
    if (projectId) params = params.set('projectId', projectId);
    if (status !== undefined && status !== null) params = params.set('status', status.toString());

    return this.api.get<ApiResponse<PagedList<ProjectTaskDto>>>(this.endpoint, params);
  }

  getTaskById(id: string): Observable<ApiResponse<ProjectTaskDto>> {
    return this.api.get<ApiResponse<ProjectTaskDto>>(`${this.endpoint}/${id}`);
  }

  createTask(dto: ProjectTaskCreateDto): Observable<ApiResponse<ProjectTaskDto>> {
    return this.api.post<ApiResponse<ProjectTaskDto>>(this.endpoint, dto);
  }

  updateTask(id: string, dto: ProjectTaskUpdateDto): Observable<ApiResponse<ProjectTaskDto>> {
    return this.api.put<ApiResponse<ProjectTaskDto>>(`${this.endpoint}/${id}`, dto);
  }

  deleteTask(id: string): Observable<ApiResponse<boolean>> {
    return this.api.delete<ApiResponse<boolean>>(`${this.endpoint}/${id}`);
  }
}
