import { Injectable, inject } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { ProjectDto, ProjectCreateDto, ProjectUpdateDto } from '../models/project.model';
import { PagedList } from '../models/paged-list.model';
import { ApiResponse } from '../models/api-response.model';
import { HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private api = inject(ApiService);
  private readonly endpoint = '/projects';

  getProjects(pageNumber = 1, pageSize = 10): Observable<ApiResponse<PagedList<ProjectDto>>> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());
    return this.api.get<ApiResponse<PagedList<ProjectDto>>>(this.endpoint, params);
  }

  getProjectById(id: string): Observable<ApiResponse<ProjectDto>> {
    return this.api.get<ApiResponse<ProjectDto>>(`${this.endpoint}/${id}`);
  }

  createProject(dto: ProjectCreateDto): Observable<ApiResponse<ProjectDto>> {
    return this.api.post<ApiResponse<ProjectDto>>(this.endpoint, dto);
  }

  updateProject(id: string, dto: ProjectUpdateDto): Observable<ApiResponse<ProjectDto>> {
    return this.api.put<ApiResponse<ProjectDto>>(`${this.endpoint}/${id}`, dto);
  }

  deleteProject(id: string): Observable<ApiResponse<boolean>> {
    return this.api.delete<ApiResponse<boolean>>(`${this.endpoint}/${id}`);
  }
}
