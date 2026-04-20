import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { NzTableModule } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTooltipModule } from 'ng-zorro-antd/tooltip';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzBadgeModule } from 'ng-zorro-antd/badge';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';
import { NzCardModule } from 'ng-zorro-antd/card';

import { ProjectService } from '../../../core/services/project.service';
import { ProjectDto, ProjectCreateDto, ProjectUpdateDto } from '../../../core/models/project.model';

@Component({
  selector: 'app-project-list',
  imports: [
    CommonModule, ReactiveFormsModule,
    NzTableModule, NzButtonModule, NzIconModule, NzModalModule,
    NzFormModule, NzInputModule, NzDatePickerModule, NzTagModule,
    NzSpinModule, NzTooltipModule, NzDividerModule, NzBadgeModule,
    NzDrawerModule, NzPopconfirmModule, NzEmptyModule, NzStatisticModule, NzCardModule,
  ],
  templateUrl: './project-list.html',
  styleUrl: './project-list.scss',
})
export class ProjectList implements OnInit {
  private projectService = inject(ProjectService);
  private notification = inject(NzNotificationService);
  private modal = inject(NzModalService);
  private fb = inject(FormBuilder);

  // ===== Signals =====
  projects = signal<ProjectDto[]>([]);
  isLoading = signal(false);
  isModalVisible = signal(false);
  isSaving = signal(false);
  editingProject = signal<ProjectDto | null>(null);
  pageNumber = signal(1);
  pageSize = signal(10);
  totalCount = signal(0);

  // ===== Computed =====
  modalTitle = computed(() =>
    this.editingProject() ? 'تعديل المشروع' : 'إضافة مشروع جديد'
  );

  totalTaskCount = computed(() =>
    this.projects().reduce((sum, p) => sum + p.taskCount, 0)
  );

  // ===== Form =====
  form!: FormGroup;

  ngOnInit(): void {
    this.buildForm();
    this.loadProjects();
  }

  buildForm(): void {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(200)]],
      description: [''],
      startDate: [null, [Validators.required]],
      endDate: [null],
    });
  }

  loadProjects(): void {
    this.isLoading.set(true);
    this.projectService.getProjects(this.pageNumber(), this.pageSize()).subscribe({
      next: (res) => {
        if (res.success && res.data) {
          this.projects.set(res.data.items);
          this.totalCount.set(res.data.totalCount);
        }
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false),
    });
  }

  openCreateModal(): void {
    this.editingProject.set(null);
    this.form.reset();
    this.isModalVisible.set(true);
  }

  openEditModal(project: ProjectDto): void {
    this.editingProject.set(project);
    this.form.patchValue({
      name: project.name,
      description: project.description,
      startDate: project.startDate ? new Date(project.startDate) : null,
      endDate: project.endDate ? new Date(project.endDate) : null,
    });
    this.isModalVisible.set(true);
  }

  closeModal(): void {
    this.isModalVisible.set(false);
  }

  disabledEndDate = (endDate: Date): boolean => {
    const startDate = this.form.get('startDate')?.value;
    if (!endDate || !startDate) return false;
    return endDate <= new Date(startDate);
  };

  saveProject(): void {
    if (this.form.invalid) {
      Object.values(this.form.controls).forEach(c => {
        c.markAsDirty();
        c.updateValueAndValidity();
      });
      return;
    }

    const val = this.form.value;
    const dto = {
      name: val.name,
      description: val.description ?? '',
      startDate: (val.startDate as Date).toISOString(),
      endDate: val.endDate ? (val.endDate as Date).toISOString() : null,
    };

    this.isSaving.set(true);
    const editing = this.editingProject();

    const request$ = editing
      ? this.projectService.updateProject(editing.id, dto as ProjectUpdateDto)
      : this.projectService.createProject(dto as ProjectCreateDto);

    request$.subscribe({
      next: (res) => {
        if (res.success) {
          this.notification.success('نجاح', editing ? 'تم تعديل المشروع بنجاح.' : 'تم إنشاء المشروع بنجاح.');
          this.closeModal();
          this.loadProjects();
        } else {
          this.notification.error('خطأ', res.message ?? 'فشلت العملية.');
        }
        this.isSaving.set(false);
      },
      error: () => this.isSaving.set(false),
    });
  }

  deleteProject(id: string): void {
    this.projectService.deleteProject(id).subscribe({
      next: (res) => {
        if (res.success) {
          this.notification.success('نجاح', 'تم حذف المشروع بنجاح.');
          this.loadProjects();
        } else {
          this.notification.error('خطأ', res.message ?? 'فشل الحذف.');
        }
      },
    });
  }

  onPageChange(page: number): void {
    this.pageNumber.set(page);
    this.loadProjects();
  }
}
