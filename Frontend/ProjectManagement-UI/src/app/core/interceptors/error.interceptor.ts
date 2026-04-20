import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const notification = inject(NzNotificationService);

  return next(req).pipe(
    catchError((error) => {
      let message = 'حدث خطأ غير متوقع، يرجى المحاولة مجدداً.';
      if (error.status === 0) message = 'لا يمكن الاتصال بالخادم، تحقق من اتصالك.';
      else if (error.status === 400) message = error.error?.message ?? 'البيانات المدخلة غير صحيحة.';
      else if (error.status === 404) message = error.error?.message ?? 'العنصر المطلوب غير موجود.';
      else if (error.status === 500) message = 'خطأ في الخادم، يرجى التواصل مع الدعم.';
      else if (error.error?.message) message = error.error.message;

      notification.error('خطأ', message, { nzDuration: 5000 });
      return throwError(() => error);
    })
  );
};
