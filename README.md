# Project Management System (نظام إدارة المشاريع)

نظام متكامل لإدارة المشاريع والمهام تم تصميمه وبناؤه بأحدث التقنيات مع التركيز على المعمارية النظيفة (Clean Architecture) وتجربة المستخدم (UX/UI) العصرية.
![ProjectHub Preview](./assets/images/Projects.png)
![TaskHub Preview](./assets/images/Lists%20Tasks.png)



## 🌟 الميزات الرئيسية (Key Features)

* **إدارة المشاريع (Projects Management):** إنشاء المشاريع وتتبعها وتحديد فتراتها الزمنية.
* **إدارة المهام (Tasks Management):** إضافة المهام وربطها بالمشاريع، مع التحكم بالحالات المختلفة (قيد الانتظار، قيد التنفيذ، مكتملة).
* **معالجة التواريخ الذكية (Smart Date Validation):**
  * منع إضافة مهام بتواريخ تقع خارج النطاق الزمني للمشروع.
  * منع تداخل التواريخ وتجاوز تاريخ الاستحقاق لتاريخ البداية.
* **واجهة مستخدم عصرية (Premium UI):** تصميم احترافي باستخدام Angular 21 و NG-ZORRO متوافق مع اللغة العربية (RTL).
* **أداء عالي (High Performance):** واجهة أمامية تعتمد كلياً على Signals، وخلفية مبنية بـ .NET مع تطبيق الـ Repository Pattern.

---

## 🛠 التكنولوجيا المستخدمة (Tech Stack)

### Backend (.NET API)
* **Framework:** ASP.NET Core Web API (أحدث إصدار)
* **Architecture:** N-Tier Architecture (Controllers, Services, Repositories).
* **Database:** SQL Server مع Entity Framework Core.
* **Patterns:**
  * Dependency Injection
  * Repository Pattern
  * DTO (Data Transfer Objects)
  * Global Exception Handling
* **Libraries:** AutoMapper، PagedList

### Frontend (Angular)
* **Framework:** Angular 21 (Standalone Components)
* **State Management:** Angular Signals
* **UI Library:** NG-ZORRO (Ant Design for Angular)
* **Styling:** CSS/SCSS (Custom Premium Tokens)
* **HTTP:** Interceptors لمعالجة الأخطاء والرسائل.

---

## ⚙️ متطلبات التشغيل (Prerequisites)

* **.NET SDK**
* **Node.js** (إصدار حديث)
* **SQL Server**

---

## 🚀 كيفية التشغيل (Getting Started)

### 1. إعداد الـ Backend (الخادم)
1. افتح الطرفية (Terminal) وانتقل إلى مجلد: \`Backend/ProjectManagement.Api\`
2. قم بتحديث قاعدة البيانات (Migrations):
   \`\`\`bash
   dotnet ef database update
   \`\`\`
3. قم بتشغيل الخادم:
   \`\`\`bash
   dotnet run
   \`\`\`
   سيتم تشغيل الـ API في الغالب على \`https://localhost:7298\`. يمكنك التحقق من واجهة Swagger عبر وضع \`/swagger\` في الرابط.

### 2. إعداد الـ Frontend (الواجهة)
1. افتح طرفية جديدة وانتقل إلى مجلد \`Frontend/ProjectManagement-UI\`.
2. قم بتثبيت الحزم المطلوبة:
   \`\`\`bash
   npm install
   \`\`\`
3. قم بتشغيل خادم التطوير لـ Angular:
   \`\`\`bash
   npx ng serve
   \`\`\`
4. افتح المتصفح على الرابط: \`http://localhost:4200\`

---
## 📂 هيكلية المشروع (Project Structure)

تبنى هذه المنظومة على مبدأ **Separation of Concerns**، مع تنظيم دقيق لكل من الجانب الأمامي والخلفي لضمان سهولة الصيانة.

### 💻 بنية الواجهة الأمامية (Frontend Structure)
*تعتمد على إطار عمل **Angular** بتنظيم موديولار (Feature-based).*

| المسار | الوصف والمسؤولية |
| :--- | :--- |
| `core/` | النماذج الأساسية، خدمات الـ API، والـ Interceptors. |
| `layout/` | العناصر الهيكلية الثابتة مثل الـ Sidebar والـ Header. |
| `features/` | الوظائف الحيوية (المشاريع والمهام) مفصولة في موديولات مستقلة. |
| `app.routes.ts` | إدارة التنقل (Routing) بين صفحات النظام. |
| `styles.scss` | نظام التصميم المتكامل (Design Tokens) لتوحيد الواجهات. |

```text
src/
└── app/
    ├── core/          # Shared services & models
    ├── layout/        # Shell components (Header/Sidebar)
    ├── features/      # Business modules
    │   ├── projects/  # Projects Management (CRUD)
    │   └── tasks/     # Tasks & Filtering logic
    ├── app.routes.ts  # Application routing
    └── styles.scss    # Global design tokens
---
---

مبني بإتقان وحب وشغف للبرمجة واهتمام بإنتاج كود نظيف واحترافي. ❤️
