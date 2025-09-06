# PROJECT REQUIREMENTS - WATER SERVICE MANAGEMENT SYSTEM

## 1. PROJECT OVERVIEW

### 1.1 Project Name
Water Service Management System (Hệ thống Quản lý Dịch vụ Nước sạch)

### 1.2 Project Description
A comprehensive web-based system for managing water service operations in a community/commune, providing both administrative management capabilities for water service providers and public inquiry functionality for residents.

### 1.3 Target Users
- **Primary Users**: Water service administrators/managers
- **Secondary Users**: Residents/customers for information inquiry

### 1.4 Project Goals
- Streamline water meter reading and billing processes
- Provide transparent water consumption information to residents
- Enable efficient management of customer accounts and payments
- Generate comprehensive reports and statistics
- Reduce manual paperwork and improve operational efficiency

## 2. FUNCTIONAL REQUIREMENTS

### 2.1 Administrative Module (Admin Panel)

#### 2.1.1 Authentication & Account Management
- **REQ-001**: Admin login with username/email and password
- **REQ-002**: Secure logout functionality
- **REQ-003**: Password management (optional: create/modify admin accounts)
- **REQ-004**: Session management and timeout

#### 2.1.2 Customer Management
- **REQ-005**: Add new customer households with complete information
  - Customer ID, household head name, address, phone number
- **REQ-006**: Edit/update customer information
- **REQ-007**: Search and filter customers by:
  - Customer ID
  - Customer name
  - Phone number
- **REQ-008**: Deactivate/delete customer accounts
- **REQ-009**: View detailed customer information and history

#### 2.1.3 Meter Reading & Billing Management
- **REQ-010**: Bulk meter reading input for all customers per billing period
- **REQ-011**: Price management per billing period (VNĐ/m³)
- **REQ-012**: Automatic consumption calculation (New reading - Previous reading)
- **REQ-013**: Automatic total amount calculation (Consumption × Unit price)
- **REQ-014**: Automatic invoice generation for all customers
- **REQ-015**: View meter reading history for specific customers
- **REQ-016**: Period selection (month/quarter) for data entry

#### 2.1.4 Payment Management
- **REQ-017**: Update payment status (Paid/Unpaid)
- **REQ-018**: Record payment date
- **REQ-019**: Track payment history per customer

#### 2.1.5 Reporting & Statistics
- **REQ-020**: Dashboard with key metrics:
  - Number of unpaid households
  - Total revenue for current period
  - Total water consumption
- **REQ-021**: Revenue reports (total due, collected, outstanding)
- **REQ-022**: Consumption reports (total water usage)
- **REQ-023**: Outstanding debt list generation
- **REQ-024**: Export reports to Excel/PDF (optional)

#### 2.1.6 Notification Management
- **REQ-025**: Create general notifications (water cuts, price changes, important information)
- **REQ-026**: Display notifications on public inquiry page

### 2.2 Public Inquiry Module (Customer Portal)

#### 2.2.1 Information Lookup
- **REQ-027**: Simple search interface with single input field
- **REQ-028**: Search by Customer ID or Phone Number
- **REQ-029**: Display comprehensive customer information on single page

#### 2.2.2 Bill Information Display
- **REQ-030**: Current period bill information:
  - Payment status (prominent display with color coding)
  - Previous and current meter readings
  - Water consumption (m³)
  - Unit price (VNĐ/m³)
  - Total amount due
- **REQ-031**: Historical consumption data for previous periods
- **REQ-032**: Contact information for support

## 3. USER INTERFACE REQUIREMENTS

### 3.1 Admin Interface Flow
1. **Login Screen**
   - Username/Email input field
   - Password input field
   - Login button
   - Error message display for failed attempts

2. **Dashboard**
   - Navigation menu (sidebar/header)
   - Quick statistics display
   - Quick action buttons
   - Navigation to main functions

3. **Customer Management**
   - Search/filter functionality
   - Customer list table
   - Add new customer button
   - Customer detail view

4. **Meter Reading & Billing**
   - Period selection
   - Unit price input
   - Bulk meter reading form
   - Save and generate invoices button

### 3.2 Public Interface Flow
1. **Home Page**
   - Clear title: "WATER SERVICE INFORMATION LOOKUP"
   - Single input field for Customer ID or Phone Number
   - Large "VIEW" button
   - Optional general notifications

2. **Results Page**
   - Customer information section
   - Current bill information (prominent)
   - Historical consumption data
   - Contact information

## 4. SYSTEM ARCHITECTURE

### 4.1 MVC Architecture Overview
The system follows the Model-View-Controller (MVC) architectural pattern to ensure separation of concerns, maintainability, and scalability.

#### 4.1.1 Architecture Layers
```
┌─────────────────────────────────────────────────────────────┐
│                    PRESENTATION LAYER                       │
│  ┌─────────────────┐  ┌─────────────────┐  ┌──────────────┐ │
│  │   Admin Views   │  │  Public Views   │  │   Mobile UI  │ │
│  │   (Dashboard,   │  │  (Search,       │  │  (Responsive │ │
│  │   Management,   │  │   Results,      │  │   Views)     │ │
│  │   Reports)      │  │   Notifications)│  │              │ │
│  └─────────────────┘  └─────────────────┘  └──────────────┘ │
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                   APPLICATION LAYER                         │
│  ┌─────────────────┐  ┌─────────────────┐  ┌──────────────┐ │
│  │ Admin Controllers│  │Public Controllers│  │   Services  │ │
│  │ (Auth, Customer,│  │ (Search,        │  │ (Business    │ │
│  │  Billing,       │  │  Display,       │  │  Logic,      │ │
│  │  Reports)       │  │  Notifications) │  │  Validation) │ │
│  └─────────────────┘  └─────────────────┘  └──────────────┘ │
└─────────────────────────────────────────────────────────────┘
┌─────────────────────────────────────────────────────────────┐
│                      DATA LAYER                             │
│  ┌─────────────────┐  ┌─────────────────┐  ┌──────────────┐ │
│  │     Models      │  │   Repositories  │  │   Database   │ │
│  │ (Customer,      │  │ (Data Access,   │  │ (MySQL/      │ │
│  │  Invoice,       │  │  CRUD Ops,      │  │  PostgreSQL) │ │
│  │  Payment, etc.) │  │  Queries)       │  │              │ │
│  └─────────────────┘  └─────────────────┘  └──────────────┘ │
└─────────────────────────────────────────────────────────────┘
```

### 4.2 Model Layer (Data & Business Logic)

#### 4.2.1 Core Models
```php
// Customer Model
class Customer {
    private $id;
    private $customerCode;
    private $householdHeadName;
    private $address;
    private $phoneNumber;
    private $status; // active, inactive
    private $createdAt;
    private $updatedAt;
    
    // Getters, setters, validation methods
    public function validate();
    public function isActive();
    public function getFullAddress();
}

// MeterReading Model
class MeterReading {
    private $id;
    private $customerId;
    private $period; // YYYY-MM format
    private $previousReading;
    private $currentReading;
    private $consumption; // calculated
    private $unitPrice;
    private $totalAmount; // calculated
    private $readingDate;
    private $createdBy; // admin user
}

// Invoice Model
class Invoice {
    private $id;
    private $customerId;
    private $meterReadingId;
    private $period;
    private $amount;
    private $paymentStatus; // paid, unpaid, overdue
    private $paymentDate;
    private $dueDate;
    private $createdAt;
}

// Payment Model
class Payment {
    private $id;
    private $invoiceId;
    private $amount;
    private $paymentDate;
    private $paymentMethod;
    private $notes;
    private $processedBy; // admin user
}

// AdminUser Model
class AdminUser {
    private $id;
    private $username;
    private $email;
    private $password; // hashed
    private $role; // admin, super_admin
    private $lastLogin;
    private $isActive;
}

// Notification Model
class Notification {
    private $id;
    private $title;
    private $content;
    private $type; // general, urgent, maintenance
    private $isActive;
    private $startDate;
    private $endDate;
    private $createdBy;
}
```

#### 4.2.2 Business Logic Services
```php
// BillingService
class BillingService {
    public function calculateConsumption($previousReading, $currentReading);
    public function calculateTotalAmount($consumption, $unitPrice);
    public function generateInvoicesForPeriod($period, $unitPrice);
    public function validateMeterReading($reading);
}

// PaymentService
class PaymentService {
    public function processPayment($invoiceId, $amount, $paymentMethod);
    public function updatePaymentStatus($invoiceId, $status);
    public function calculateOutstandingAmount($customerId);
    public function generatePaymentReceipt($paymentId);
}

// ReportService
class ReportService {
    public function generateRevenueReport($startDate, $endDate);
    public function generateConsumptionReport($period);
    public function generateOutstandingDebtList();
    public function generateCustomerStatistics();
}

// NotificationService
class NotificationService {
    public function createNotification($title, $content, $type);
    public function getActiveNotifications();
    public function deactivateNotification($notificationId);
}
```

### 4.3 View Layer (Presentation)

#### 4.3.1 Admin Views Structure
```
admin/
├── layouts/
│   ├── main.blade.php          # Main admin layout
│   ├── sidebar.blade.php       # Navigation sidebar
│   └── header.blade.php        # Top header
├── auth/
│   ├── login.blade.php         # Admin login form
│   └── logout.blade.php        # Logout confirmation
├── dashboard/
│   └── index.blade.php         # Main dashboard
├── customers/
│   ├── index.blade.php         # Customer list
│   ├── create.blade.php        # Add new customer
│   ├── edit.blade.php          # Edit customer
│   └── show.blade.php          # Customer details
├── billing/
│   ├── meter-reading.blade.php # Bulk meter reading
│   ├── invoices.blade.php      # Invoice management
│   └── payments.blade.php      # Payment tracking
├── reports/
│   ├── revenue.blade.php       # Revenue reports
│   ├── consumption.blade.php   # Consumption reports
│   └── outstanding.blade.php   # Outstanding debts
└── notifications/
    ├── index.blade.php         # Notification list
    ├── create.blade.php        # Create notification
    └── edit.blade.php          # Edit notification
```

#### 4.3.2 Public Views Structure
```
public/
├── layouts/
│   └── public.blade.php        # Public site layout
├── home/
│   └── index.blade.php         # Search form
├── search/
│   └── results.blade.php       # Search results
└── components/
    ├── search-form.blade.php   # Reusable search form
    ├── bill-display.blade.php  # Bill information display
    └── notification-banner.blade.php # Notification display
```

#### 4.3.3 View Components
```php
// Search Form Component
@component('components.search-form')
    @slot('action', route('public.search'))
    @slot('placeholder', 'Nhập mã khách hàng hoặc số điện thoại')
@endcomponent

// Bill Display Component
@component('components.bill-display', ['invoice' => $invoice])
    @slot('highlight', true)
    @slot('showHistory', true)
@endcomponent

// Notification Banner Component
@component('components.notification-banner')
    @slot('notifications', $activeNotifications)
@endcomponent
```

### 4.4 Controller Layer (Application Logic)

#### 4.4.1 Admin Controllers
```php
// AdminAuthController
class AdminAuthController extends Controller {
    public function showLoginForm();
    public function login(Request $request);
    public function logout();
    public function dashboard();
}

// CustomerController
class CustomerController extends Controller {
    public function index(Request $request);
    public function create();
    public function store(Request $request);
    public function show($id);
    public function edit($id);
    public function update(Request $request, $id);
    public function destroy($id);
    public function search(Request $request);
}

// BillingController
class BillingController extends Controller {
    public function meterReading();
    public function storeMeterReading(Request $request);
    public function invoices(Request $request);
    public function generateInvoices(Request $request);
    public function updatePaymentStatus(Request $request);
}

// ReportController
class ReportController extends Controller {
    public function dashboard();
    public function revenue(Request $request);
    public function consumption(Request $request);
    public function outstanding();
    public function export(Request $request);
}

// NotificationController
class NotificationController extends Controller {
    public function index();
    public function create();
    public function store(Request $request);
    public function edit($id);
    public function update(Request $request, $id);
    public function destroy($id);
}
```

#### 4.4.2 Public Controllers
```php
// PublicController
class PublicController extends Controller {
    public function index();
    public function search(Request $request);
    public function results($customerId);
    public function getNotifications();
}

// SearchController
class SearchController extends Controller {
    public function search(Request $request);
    public function validateSearchInput($input);
    public function findCustomer($searchTerm);
}
```

### 4.5 Repository Layer (Data Access)

#### 4.5.1 Repository Interfaces
```php
interface CustomerRepositoryInterface {
    public function findAll($filters = []);
    public function findById($id);
    public function findByCode($code);
    public function findByPhone($phone);
    public function create(array $data);
    public function update($id, array $data);
    public function delete($id);
    public function search($term);
}

interface InvoiceRepositoryInterface {
    public function findByCustomer($customerId);
    public function findByPeriod($period);
    public function findUnpaid();
    public function create(array $data);
    public function updatePaymentStatus($id, $status);
    public function getRevenueReport($startDate, $endDate);
}

interface MeterReadingRepositoryInterface {
    public function findByCustomer($customerId);
    public function findByPeriod($period);
    public function createBulk(array $readings);
    public function getLatestReading($customerId);
}
```

#### 4.5.2 Repository Implementations
```php
class EloquentCustomerRepository implements CustomerRepositoryInterface {
    protected $model;
    
    public function __construct(Customer $model) {
        $this->model = $model;
    }
    
    public function findAll($filters = []) {
        $query = $this->model->query();
        
        if (isset($filters['status'])) {
            $query->where('status', $filters['status']);
        }
        
        if (isset($filters['search'])) {
            $query->where(function($q) use ($filters) {
                $q->where('customer_code', 'like', '%' . $filters['search'] . '%')
                  ->orWhere('household_head_name', 'like', '%' . $filters['search'] . '%')
                  ->orWhere('phone_number', 'like', '%' . $filters['search'] . '%');
            });
        }
        
        return $query->paginate(20);
    }
    
    // Other methods implementation...
}
```

### 4.6 Routing Structure

#### 4.6.1 Admin Routes
```php
// Admin routes (protected by auth middleware)
Route::prefix('admin')->middleware(['auth:admin'])->group(function () {
    Route::get('/', 'AdminController@dashboard')->name('admin.dashboard');
    
    // Customer management
    Route::resource('customers', 'CustomerController');
    Route::get('customers/search', 'CustomerController@search')->name('customers.search');
    
    // Billing management
    Route::get('billing/meter-reading', 'BillingController@meterReading')->name('billing.meter-reading');
    Route::post('billing/meter-reading', 'BillingController@storeMeterReading')->name('billing.store-meter-reading');
    Route::resource('invoices', 'InvoiceController');
    
    // Reports
    Route::get('reports/revenue', 'ReportController@revenue')->name('reports.revenue');
    Route::get('reports/consumption', 'ReportController@consumption')->name('reports.consumption');
    Route::get('reports/outstanding', 'ReportController@outstanding')->name('reports.outstanding');
    
    // Notifications
    Route::resource('notifications', 'NotificationController');
});

// Admin authentication routes
Route::prefix('admin')->group(function () {
    Route::get('login', 'AdminAuthController@showLoginForm')->name('admin.login');
    Route::post('login', 'AdminAuthController@login');
    Route::post('logout', 'AdminAuthController@logout')->name('admin.logout');
});
```

#### 4.6.2 Public Routes
```php
// Public routes (no authentication required)
Route::get('/', 'PublicController@index')->name('public.home');
Route::post('search', 'PublicController@search')->name('public.search');
Route::get('results/{customerId}', 'PublicController@results')->name('public.results');
Route::get('notifications', 'PublicController@getNotifications')->name('public.notifications');
```

### 4.7 Middleware and Security

#### 4.7.1 Custom Middleware
```php
// AdminAuthMiddleware
class AdminAuthMiddleware {
    public function handle($request, Closure $next) {
        if (!auth('admin')->check()) {
            return redirect()->route('admin.login');
        }
        return $next($request);
    }
}

// RateLimitingMiddleware
class RateLimitingMiddleware {
    public function handle($request, Closure $next) {
        $key = $request->ip();
        $maxAttempts = 10; // per minute
        
        if (RateLimiter::tooManyAttempts($key, $maxAttempts)) {
            return response('Too many requests', 429);
        }
        
        RateLimiter::hit($key, 60);
        return $next($request);
    }
}
```

### 4.8 Configuration and Environment

#### 4.8.1 Application Configuration
```php
// config/water_service.php
return [
    'billing' => [
        'default_unit_price' => 5000, // VNĐ per m³
        'billing_period' => 'monthly', // monthly, quarterly
        'due_days' => 15, // days after billing
    ],
    'search' => [
        'max_results' => 1,
        'rate_limit' => 10, // requests per minute
    ],
    'reports' => [
        'export_formats' => ['pdf', 'excel'],
        'max_records' => 10000,
    ],
];
```

## 5. TECHNICAL REQUIREMENTS

### 5.1 Performance Requirements
- **REQ-033**: System should handle at least 1000+ customer records
- **REQ-034**: Page load time should be under 3 seconds
- **REQ-035**: Bulk data entry should process efficiently

### 5.2 Security Requirements
- **REQ-036**: Secure authentication for admin access
- **REQ-037**: Data encryption for sensitive information
- **REQ-038**: Input validation and sanitization
- **REQ-039**: Protection against common web vulnerabilities

### 5.3 Data Requirements
- **REQ-040**: Customer data storage and management
- **REQ-041**: Meter reading history storage
- **REQ-042**: Payment status tracking
- **REQ-043**: Invoice generation and storage
- **REQ-044**: Data backup and recovery

### 5.4 Browser Compatibility
- **REQ-045**: Support for modern web browsers (Chrome, Firefox, Safari, Edge)
- **REQ-046**: Responsive design for mobile devices

## 6. NON-FUNCTIONAL REQUIREMENTS

### 6.1 Usability
- **REQ-047**: Intuitive user interface for administrators
- **REQ-048**: Simple, single-purpose interface for public users
- **REQ-049**: Clear navigation and information hierarchy
- **REQ-050**: Vietnamese language support

### 6.2 Reliability
- **REQ-051**: System availability of 99% during business hours
- **REQ-052**: Data integrity and consistency
- **REQ-053**: Error handling and user feedback

### 6.3 Maintainability
- **REQ-054**: Modular code structure
- **REQ-055**: Comprehensive documentation
- **REQ-056**: Easy system updates and modifications

## 7. DATA MODEL REQUIREMENTS

### 7.1 Core Entities
- **Customer/Household**: ID, name, address, phone, status
- **Meter Reading**: Customer ID, period, previous reading, current reading
- **Invoice**: Customer ID, period, consumption, unit price, total amount, payment status
- **Payment**: Invoice ID, payment date, amount
- **Admin User**: Username, password, permissions
- **Notification**: Title, content, date, status

### 7.2 Data Relationships
- One customer can have multiple meter readings
- One meter reading generates one invoice
- One invoice can have one payment record
- Multiple notifications can be active

## 8. INTEGRATION REQUIREMENTS

### 8.1 External Systems
- **REQ-057**: Optional integration with accounting systems
- **REQ-058**: Optional SMS/Email notification system
- **REQ-059**: Optional payment gateway integration

## 9. DEPLOYMENT REQUIREMENTS

### 9.1 Environment
- **REQ-060**: Web-based application deployment
- **REQ-061**: Database server setup
- **REQ-062**: Web server configuration
- **REQ-063**: SSL certificate for secure access

### 9.2 Training & Support
- **REQ-064**: Admin user training materials
- **REQ-065**: User manual for administrators
- **REQ-066**: Technical support documentation

## 10. ACCEPTANCE CRITERIA

### 10.1 Admin Module
- Admin can successfully log in and access all functions
- Customer management operations work correctly
- Bulk meter reading and invoice generation functions properly
- Reports and statistics display accurate data
- Payment status updates work correctly

### 10.2 Public Module
- Residents can search and find their information using Customer ID or phone
- All bill information displays correctly
- Historical data shows properly
- Interface is simple and user-friendly

### 10.3 System Performance
- All operations complete within acceptable time limits
- System handles expected data volume
- No data loss or corruption occurs
- Security measures function properly

## 11. PROJECT CONSTRAINTS

### 11.1 Budget Constraints
- Cost-effective solution suitable for small to medium water service providers
- Open-source technology stack preferred

### 11.2 Time Constraints
- Phased development approach
- Priority on core functionality first

### 11.3 Technical Constraints
- Must be web-based for accessibility
- Should work on standard hardware
- Minimal IT infrastructure requirements

---

**Document Version**: 1.0  
**Last Updated**: [Current Date]  
**Prepared By**: System Analyst  
**Approved By**: Project Stakeholder