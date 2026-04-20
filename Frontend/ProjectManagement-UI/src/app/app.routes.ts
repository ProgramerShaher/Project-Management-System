import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./layout/main-layout/main-layout').then(m => m.MainLayout),
    children: [
      {
        path: '',
        redirectTo: 'projects',
        pathMatch: 'full',
      },
      {
        path: 'projects',
        loadComponent: () => import('./features/projects/project-list/project-list').then(m => m.ProjectList),
      },
      {
        path: 'tasks',
        loadComponent: () => import('./features/tasks/task-list/task-list').then(m => m.TaskList),
      },
    ],
  },
  { path: '**', redirectTo: '' },
];
