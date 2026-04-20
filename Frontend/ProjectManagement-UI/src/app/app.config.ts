import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideNzIcons } from 'ng-zorro-antd/icon';
import { ar_EG, provideNzI18n } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import ar from '@angular/common/locales/ar';
import {
  ProjectOutline,
  CheckSquareOutline,
  DashboardOutline,
  PlusOutline,
  EditOutline,
  DeleteOutline,
  EyeOutline,
  FilterOutline,
  ReloadOutline,
  CheckCircleOutline,
  ClockCircleOutline,
  SyncOutline,
  CalendarOutline,
  FolderOutline,
  MenuFoldOutline,
  MenuUnfoldOutline,
  BarsOutline,
  ExclamationCircleOutline,
  CloseOutline,
  SaveOutline,
} from '@ant-design/icons-angular/icons';

import { routes } from './app.routes';
import { errorInterceptor } from './core/interceptors/error.interceptor';

registerLocaleData(ar);

const icons = [
  ProjectOutline, CheckSquareOutline, DashboardOutline, PlusOutline,
  EditOutline, DeleteOutline, EyeOutline, FilterOutline, ReloadOutline,
  CheckCircleOutline, ClockCircleOutline, SyncOutline, CalendarOutline,
  FolderOutline, MenuFoldOutline, MenuUnfoldOutline, BarsOutline,
  ExclamationCircleOutline, CloseOutline, SaveOutline,
];

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(withInterceptors([errorInterceptor])),
    provideAnimations(),
    provideNzI18n(ar_EG),
    provideNzIcons(icons),
  ],
};
