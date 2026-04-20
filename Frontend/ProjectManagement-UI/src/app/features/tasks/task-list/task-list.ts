import { Component, inject, signal, computed, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { NzTableModule } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';

import { TaskService } from '../../../core/services/task.service';
import { ProjectService } from '../../../core/services/project.service';
import { ProjectTaskDto, ProjectTaskCreateDto, ProjectTaskUpdateDto, ProjectTaskStatus } from '../../../core/models/project-task.model';
import { ProjectDto } from '../../../core/models/project.model';
import { NzTooltipModule } from 'ng-zorro-antd/tooltip';

@Component({
  selector: 'app-task-list',
  imports: [
    CommonModule, FormsModule, ReactiveFormsModule,
    NzTableModule, NzButtonModule, NzIconModule, NzFormModule,
    NzInputModule, NzDatePickerModule, NzTagModule, NzSpinModule,
    NzTooltipModule, NzDividerModule, NzDrawerModule, NzPopconfirmModule,
    NzSelectModule, NzCardModule, NzStatisticModule,
  ],
  templateUrl: './task-list.html',
  styleUrl: './task-list.scss',
})
export class TaskList implements OnInit {
  private taskService = inject(TaskService);
  private projectService = inject(ProjectService);
  private notification = inject(NzNotificationService);
  private fb = inject(FormBuilder);

  // ===== Signals =====
  tasks = signal<ProjectTaskDto[]>([]);
  projects = signal<ProjectDto[]>([]);
  isLoading = signal(false);
  isDrawerVisible = signal(false);
  isSaving = signal(false);
  editingTask = signal<ProjectTaskDto | null>(null);
  pageNumber = signal(1);
  pageSize = signal(10);
  totalCount = signal(0);

  // filters
  filterProjectId = signal<string | null>(null);
  filterStatus = signal<ProjectTaskStatus | null>(null);

  // Enum for template
  TaskStatus = ProjectTaskStatus;

  statuses = [
    { value: ProjectTaskStatus.Pending, label: 'قيد الانتظار', color: 'default' },
    { value: ProjectTaskStatus.InProgress, label: 'قيد التنفيذ', color: 'processing' },
    { value: ProjectTaskStatus.Completed, label: 'مكتملة', color: 'success' },
  ];

  drawerTitle = computed(() =>
    this.editingTask() ? 'تعديل المهمة' : 'إضافة مهمة جديدة'
  );

  selectedProject = computed(() => {
    const id = this.form?.get('projectId')?.value;
    return this.projects().find(p => p.id === id) ?? null;
  });

  form!: FormGroup;

  ngOnInit(): void {
    this.buildForm();
    this.loadTasks();
    this.loadProjects();
  }

  buildForm(): void {
    this.form = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      description: [''],
      status: [ProjectTaskStatus.Pending, [Validators.required]],
      projectId: [null, [Validators.required]],
      startDate: [null, [Validators.required]],
      dueDate: [null, [Validators.required]],
    });
  }

  loadProjects(): void {
    this.projectService.getProjects(1, 100).subscribe({
      next: (res) => {
        if (res.success && res.data) this.projects.set(res.data.items);
      },
    });
  }

  loadTasks(): void {
    this.isLoading.set(true);
    this.taskService.getTasks(
      this.filterProjectId() ?? undefined,
      this.filterStatus() ?? undefined,
      this.pageNumber(),
      this.pageSize()
    ).subscribe({
      next: (res) => {
        if (res.success && res.data) {
          this.tasks.set(res.data.items);
          this.totalCount.set(res.data.totalCount);
        }
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false),
    });
  }

  onFilterChange(): void {
    this.pageNumber.set(1);
    this.loadTasks();
  }

  resetFilters(): void {
    this.filterProjectId.set(null);
    this.filterStatus.set(null);
    this.loadTasks();
  }

  openCreateDrawer(): void {
    this.editingTask.set(null);
    this.form.reset({ status: ProjectTaskStatus.Pending });
    this.isDrawerVisible.set(true);
  }

  openEditDrawer(task: ProjectTaskDto): void {
    this.editingTask.set(task);
    this.form.patchValue({
      title: task.title,
      description: task.description,
      status: task.status,
      projectId: task.projectId,
      startDate: new Date(task.startDate),
      dueDate: new Date(task.dueDate),
    });
    this.isDrawerVisible.set(true);
  }

  closeDrawer(): void {
    this.isDrawerVisible.set(false);
  }

  getProjectForTask(): ProjectDto | null {
    const id = this.form.get('projectId')?.value;
    return this.projects().find(p => p.id === id) ?? null;
  }

  disabledStartDate = (d: Date): boolean => {
    const proj = this.getProjectForTask();
    if (!proj) return false;
    const projStart = new Date(proj.startDate);
    projStart.setHours(0, 0, 0, 0);
    d.setHours(0, 0, 0, 0);
    return d < projStart;
  };

  disabledDueDate = (d: Date): boolean => {
    const start = this.form.get('startDate')?.value;
    const proj = this.getProjectForTask();
    const afterStart = start ? d <= new Date(start) : false;
    const afterProjEnd = proj?.endDate ? d > new Date(proj.endDate) : false;
    return afterStart || afterProjEnd;
  };

  saveTask(): void {
    if (this.form.invalid) {
      Object.values(this.form.controls).forEach(c => {
        c.markAsDirty();
        c.updateValueAndValidity();
      });
      return;
    }

    const val = this.form.value;
    this.isSaving.set(true);
    const editing = this.editingTask();

    if (editing) {
      const dto: ProjectTaskUpdateDto = {
        title: val.title,
        description: val.description ?? '',
        status: val.status,
        startDate: (val.startDate as Date).toISOString(),
        dueDate: (val.dueDate as Date).toISOString(),
      };
      this.taskService.updateTask(editing.id, dto).subscribe({
        next: (res) => {
          if (res.success) {
            this.notification.success('نجاح', 'تم تعديل المهمة بنجاح.');
            this.closeDrawer();
            this.loadTasks();
          } else {
            this.notification.error('خطأ في التواريخ', res.message ?? '');
          }
          this.isSaving.set(false);
        },
        error: () => this.isSaving.set(false),
      });
    } else {
      const dto: ProjectTaskCreateDto = {
        title: val.title,
        description: val.description ?? '',
        status: val.status,
        projectId: val.projectId,
        startDate: (val.startDate as Date).toISOString(),
        dueDate: (val.dueDate as Date).toISOString(),
      };
      this.taskService.createTask(dto).subscribe({
        next: (res) => {
          if (res.success) {
            this.notification.success('نجاح', 'تمت إضافة المهمة بنجاح.');
            this.closeDrawer();
            this.loadTasks();
          } else {
            this.notification.error('خطأ في التواريخ', res.message ?? '');
          }
          this.isSaving.set(false);
        },
        error: () => this.isSaving.set(false),
      });
    }
  }

  deleteTask(id: string): void {
    this.taskService.deleteTask(id).subscribe({
      next: (res) => {
        if (res.success) {
          this.notification.success('نجاح', 'تم حذف المهمة.');
          this.loadTasks();
        }
      },
    });
  }

  getStatusLabel(status: ProjectTaskStatus): string {
    return this.statuses.find(s => s.value === status)?.label ?? '';
  }

  getStatusColor(status: ProjectTaskStatus): string {
    return this.statuses.find(s => s.value === status)?.color ?? 'default';
  }

  getProjectName(id: string): string {
    return this.projects().find(p => p.id === id)?.name ?? '—';
  }

  onPageChange(page: number): void {
    this.pageNumber.set(page);
    this.loadTasks();
  }
}
