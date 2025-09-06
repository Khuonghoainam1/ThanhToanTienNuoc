# PAGE 3: CUSTOMER MANAGEMENT - DETAILED PLAN

## Overview
The Customer Management page provides comprehensive tools for water service administrators to manage the complete customer lifecycle, from registration to account maintenance and historical tracking.

## Page Information
- **Route**: `/admin/customers`
- **Controller**: `CustomerController@index`
- **View**: `admin/customers/index.blade.php`
- **Access Level**: Admin users only
- **Layout**: Main admin layout with sidebar navigation

## 1. PAGE STRUCTURE & LAYOUT

### 1.1 Header Section
```
┌─────────────────────────────────────────────────────────────┐
│ Customer Management                    [Add Customer] [+]   │
└─────────────────────────────────────────────────────────────┘
```

### 1.2 Main Content Area
```
┌─────────────────────────────────────────────────────────────┐
│ Sidebar │                Customer Management                │
│         │                                                  │
│ [Menu]  │  ┌─────────────────────────────────────────────┐  │
│         │  │              Search & Filters               │  │
│         │  └─────────────────────────────────────────────┘  │
│         │                                                  │
│         │  ┌─────────────────────────────────────────────┐  │
│         │  │              Customer List Table            │  │
│         │  │  [Sortable Headers] [Pagination]           │  │
│         │  └─────────────────────────────────────────────┘  │
│         │                                                  │
│         │  ┌─────────────────────────────────────────────┐  │
│         │  │              Bulk Actions                   │  │
│         │  └─────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
```

## 2. SEARCH & FILTER FUNCTIONALITY

### 2.1 Search Interface
```php
// Search Form Structure
<form method="GET" action="{{ route('customers.index') }}" class="search-form">
    <div class="search-container">
        <input type="text" 
               name="search" 
               placeholder="Search by Customer ID, Name, or Phone..."
               value="{{ request('search') }}"
               class="search-input">
        <button type="submit" class="search-button">
            <i class="fas fa-search"></i>
        </button>
    </div>
</form>
```

### 2.2 Advanced Filters
```
┌─────────────────────────────────────────────────────────────┐
│                    Advanced Filters                         │
├─────────────────────────────────────────────────────────────┤
│ Status: [All ▼] | Registration: [All ▼] | Area: [All ▼]    │
│                                                             │
│ Date Range: [From] [To] | Payment Status: [All ▼]          │
│                                                             │
│ [Apply Filters] [Clear All] [Export Results]               │
└─────────────────────────────────────────────────────────────┘
```

### 2.3 Filter Options
- **Status Filter**: Active, Inactive, All
- **Registration Period**: Last 30 days, Last 3 months, Last year, Custom range
- **Geographic Area**: Dropdown with predefined areas
- **Payment Status**: Current, Overdue, All paid, All unpaid
- **Customer Type**: Residential, Commercial (if applicable)

## 3. CUSTOMER LIST TABLE

### 3.1 Table Structure
```php
// Customer Table Columns
$columns = [
    'checkbox' => 'Select',
    'customer_code' => 'Customer ID',
    'household_head_name' => 'Customer Name',
    'phone_number' => 'Phone',
    'address' => 'Address',
    'status' => 'Status',
    'last_payment' => 'Last Payment',
    'outstanding_amount' => 'Outstanding',
    'actions' => 'Actions'
];
```

### 3.2 Table Display Format
```
┌─────────────────────────────────────────────────────────────┐
│ ☐ │ ID      │ Name           │ Phone        │ Status │ Actions │
├───┼─────────┼────────────────┼──────────────┼────────┼─────────┤
│ ☐ │ C001234 │ Nguyen Van A   │ 0901234567   │ Active │ [View]  │
│ ☐ │ C001235 │ Tran Thi B     │ 0901234568   │ Active │ [View]  │
│ ☐ │ C001236 │ Le Van C       │ 0901234569   │ Inactive│ [View]  │
└───┴─────────┴────────────────┴──────────────┴────────┴─────────┘
```

### 3.3 Table Features
- **Sortable Columns**: Click headers to sort by any column
- **Pagination**: 20 customers per page with navigation
- **Row Selection**: Checkbox for bulk operations
- **Responsive Design**: Horizontal scroll on mobile
- **Status Indicators**: Color-coded status badges
- **Quick Actions**: View, Edit, Deactivate buttons per row

## 4. CUSTOMER DETAIL VIEW

### 4.1 Customer Information Modal/Page
```php
// Customer Detail Data Structure
$customerDetail = [
    'basic_info' => [
        'customer_code' => 'C001234',
        'household_head_name' => 'Nguyen Van A',
        'address' => '123 Main Street, District 1, HCMC',
        'phone_number' => '0901234567',
        'email' => 'nguyenvana@email.com',
        'registration_date' => '2023-01-15',
        'status' => 'Active'
    ],
    'billing_info' => [
        'current_balance' => 250000,
        'last_payment_date' => '2024-01-15',
        'payment_method' => 'Bank Transfer',
        'billing_address' => 'Same as residence'
    ],
    'consumption_history' => [
        'average_monthly_consumption' => 15.5,
        'highest_consumption' => 25.0,
        'lowest_consumption' => 8.0,
        'total_consumption_ytd' => 186.0
    ]
];
```

### 4.2 Detail View Layout
```
┌─────────────────────────────────────────────────────────────┐
│                    Customer Details                         │
├─────────────────────────────────────────────────────────────┤
│ ┌─────────────────┐ ┌─────────────────┐ ┌─────────────────┐ │
│ │   Basic Info    │ │  Billing Info   │ │   Consumption   │ │
│ │                 │ │                 │ │    History      │ │
│ │ ID: C001234     │ │ Balance: ₫250K  │ │ Avg: 15.5 m³    │ │
│ │ Name: Nguyen A  │ │ Last: Jan 15    │ │ High: 25.0 m³   │ │
│ │ Phone: 090...   │ │ Method: Bank    │ │ Low: 8.0 m³     │ │
│ │ Status: Active  │ │ Address: Same   │ │ YTD: 186.0 m³   │ │
│ └─────────────────┘ └─────────────────┘ └─────────────────┘ │
│                                                             │
│ ┌─────────────────────────────────────────────────────────┐ │
│ │                Payment History                          │ │
│ │ Date       │ Period    │ Amount    │ Status   │ Method  │ │
│ │ 2024-01-15 │ 2024-01   │ ₫250,000  │ Paid     │ Bank    │ │
│ │ 2023-12-15 │ 2023-12   │ ₫180,000  │ Paid     │ Cash    │ │
│ │ 2023-11-15 │ 2023-11   │ ₫220,000  │ Paid     │ Bank    │ │
│ └─────────────────────────────────────────────────────────┘ │
│                                                             │
│ ┌─────────────────────────────────────────────────────────┐ │
│ │                Meter Reading History                    │ │
│ │ Period    │ Previous │ Current │ Consumption │ Amount   │ │
│ │ 2024-01   │ 1,250    │ 1,265   │ 15 m³       │ ₫250K    │ │
│ │ 2023-12   │ 1,235    │ 1,250   │ 15 m³       │ ₫180K    │ │
│ │ 2023-11   │ 1,220    │ 1,235   │ 15 m³       │ ₫220K    │ │
│ └─────────────────────────────────────────────────────────┘ │
│                                                             │
│ [Edit Customer] [View Full History] [Print Statement]      │
└─────────────────────────────────────────────────────────────┘
```

## 5. ADD/EDIT CUSTOMER FORMS

### 5.1 Add Customer Form
```php
// Add Customer Form Fields
$formFields = [
    'customer_code' => [
        'type' => 'text',
        'label' => 'Customer ID',
        'required' => true,
        'auto_generate' => true,
        'format' => 'C######'
    ],
    'household_head_name' => [
        'type' => 'text',
        'label' => 'Household Head Name',
        'required' => true,
        'max_length' => 100
    ],
    'address' => [
        'type' => 'textarea',
        'label' => 'Address',
        'required' => true,
        'rows' => 3
    ],
    'phone_number' => [
        'type' => 'tel',
        'label' => 'Phone Number',
        'required' => true,
        'pattern' => '^[0-9]{10,11}$'
    ],
    'email' => [
        'type' => 'email',
        'label' => 'Email Address',
        'required' => false
    ],
    'registration_date' => [
        'type' => 'date',
        'label' => 'Registration Date',
        'required' => true,
        'default' => 'today'
    ],
    'notes' => [
        'type' => 'textarea',
        'label' => 'Additional Notes',
        'required' => false,
        'rows' => 2
    ]
];
```

### 5.2 Form Layout
```
┌─────────────────────────────────────────────────────────────┐
│                    Add New Customer                         │
├─────────────────────────────────────────────────────────────┤
│ Customer ID:     [C001237        ] [Auto-generate]          │
│ Name:            [Nguyen Van D   ]                          │
│ Address:         [456 Second St  ]                          │
│                  [District 2, HCMC]                         │
│ Phone:           [0901234570     ]                          │
│ Email:           [nguyenvand@... ]                          │
│ Registration:    [2024-01-20     ]                          │
│ Notes:           [Special requirements...]                  │
│                                                             │
│ [Save Customer] [Cancel] [Save & Add Another]              │
└─────────────────────────────────────────────────────────────┘
```

### 5.3 Form Validation
```php
// Validation Rules
$validationRules = [
    'customer_code' => 'required|unique:customers,customer_code|regex:/^C\d{6}$/',
    'household_head_name' => 'required|string|max:100',
    'address' => 'required|string|max:500',
    'phone_number' => 'required|string|regex:/^[0-9]{10,11}$/',
    'email' => 'nullable|email|max:255',
    'registration_date' => 'required|date|before_or_equal:today',
    'notes' => 'nullable|string|max:1000'
];
```

## 6. BULK OPERATIONS

### 6.1 Bulk Action Options
```php
// Bulk Actions Available
$bulkActions = [
    'export_selected' => 'Export Selected',
    'send_notification' => 'Send Notification',
    'update_status' => 'Update Status',
    'generate_reports' => 'Generate Reports',
    'delete_selected' => 'Delete Selected'
];
```

### 6.2 Bulk Action Interface
```
┌─────────────────────────────────────────────────────────────┐
│ Selected: 5 customers                                       │
│                                                             │
│ [Export Selected ▼] [Send Notification ▼] [Update Status ▼] │
│                                                             │
│ ┌─────────────────────────────────────────────────────────┐ │
│ │ Send Notification                                       │ │
│ │ Type: [General ▼] Subject: [Payment Reminder]          │ │
│ │ Message: [Your payment is overdue...]                  │ │
│ │ [Send] [Cancel]                                         │ │
│ └─────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────┘
```

## 7. RESPONSIVE DESIGN SPECIFICATIONS

### 7.1 Desktop Layout (≥1200px)
- **Table**: Full table with all columns visible
- **Filters**: Horizontal filter bar
- **Actions**: Inline action buttons
- **Modal**: Large modal for customer details

### 7.2 Tablet Layout (768px - 1199px)
- **Table**: Horizontal scroll for table
- **Filters**: Collapsible filter section
- **Actions**: Dropdown menu for actions
- **Modal**: Medium-sized modal

### 7.3 Mobile Layout (<768px)
- **Table**: Card-based layout instead of table
- **Filters**: Accordion-style filters
- **Actions**: Bottom action bar
- **Modal**: Full-screen modal

### 7.4 Mobile Card Layout
```
┌─────────────────────────────────────────────────────────────┐
│ Customer Card                                               │
├─────────────────────────────────────────────────────────────┤
│ ☐ C001234 - Nguyen Van A                                   │
│ 📞 0901234567 | 📍 123 Main Street                         │
│ Status: Active | Outstanding: ₫250,000                     │
│ [View] [Edit] [More ▼]                                     │
└─────────────────────────────────────────────────────────────┘
```

## 8. TECHNICAL IMPLEMENTATION

### 8.1 Controller Methods
```php
class CustomerController extends Controller
{
    public function index(Request $request)
    {
        $query = Customer::query();
        
        // Apply search filter
        if ($request->filled('search')) {
            $search = $request->search;
            $query->where(function($q) use ($search) {
                $q->where('customer_code', 'like', "%{$search}%")
                  ->orWhere('household_head_name', 'like', "%{$search}%")
                  ->orWhere('phone_number', 'like', "%{$search}%");
            });
        }
        
        // Apply status filter
        if ($request->filled('status')) {
            $query->where('status', $request->status);
        }
        
        // Apply date range filter
        if ($request->filled('date_from')) {
            $query->where('created_at', '>=', $request->date_from);
        }
        
        $customers = $query->with(['invoices' => function($q) {
            $q->latest()->limit(1);
        }])->paginate(20);
        
        return view('admin.customers.index', compact('customers'));
    }
    
    public function show($id)
    {
        $customer = Customer::with([
            'invoices.payments',
            'meterReadings'
        ])->findOrFail($id);
        
        return view('admin.customers.show', compact('customer'));
    }
    
    public function create()
    {
        return view('admin.customers.create');
    }
    
    public function store(Request $request)
    {
        $validated = $request->validate([
            'customer_code' => 'required|unique:customers,customer_code',
            'household_head_name' => 'required|string|max:100',
            'address' => 'required|string|max:500',
            'phone_number' => 'required|string|regex:/^[0-9]{10,11}$/',
            'email' => 'nullable|email|max:255',
            'registration_date' => 'required|date',
            'notes' => 'nullable|string|max:1000'
        ]);
        
        $customer = Customer::create($validated);
        
        return redirect()
            ->route('customers.show', $customer)
            ->with('success', 'Customer created successfully');
    }
    
    public function edit($id)
    {
        $customer = Customer::findOrFail($id);
        return view('admin.customers.edit', compact('customer'));
    }
    
    public function update(Request $request, $id)
    {
        $customer = Customer::findOrFail($id);
        
        $validated = $request->validate([
            'household_head_name' => 'required|string|max:100',
            'address' => 'required|string|max:500',
            'phone_number' => 'required|string|regex:/^[0-9]{10,11}$/',
            'email' => 'nullable|email|max:255',
            'status' => 'required|in:active,inactive',
            'notes' => 'nullable|string|max:1000'
        ]);
        
        $customer->update($validated);
        
        return redirect()
            ->route('customers.show', $customer)
            ->with('success', 'Customer updated successfully');
    }
    
    public function destroy($id)
    {
        $customer = Customer::findOrFail($id);
        
        // Check if customer has any invoices
        if ($customer->invoices()->exists()) {
            return back()->with('error', 'Cannot delete customer with existing invoices');
        }
        
        $customer->delete();
        
        return redirect()
            ->route('customers.index')
            ->with('success', 'Customer deleted successfully');
    }
    
    public function bulkAction(Request $request)
    {
        $action = $request->action;
        $customerIds = $request->customer_ids;
        
        switch ($action) {
            case 'export_selected':
                return $this->exportCustomers($customerIds);
            case 'send_notification':
                return $this->sendBulkNotification($customerIds, $request->notification_data);
            case 'update_status':
                return $this->updateBulkStatus($customerIds, $request->status);
            default:
                return back()->with('error', 'Invalid action');
        }
    }
}
```

### 8.2 Blade Template Structure
```php
@extends('admin.layouts.main')

@section('title', 'Customer Management')

@section('content')
<div class="customer-management">
    <!-- Header -->
    <div class="page-header">
        <h1>Customer Management</h1>
        <a href="{{ route('customers.create') }}" class="btn btn-primary">
            <i class="fas fa-plus"></i> Add Customer
        </a>
    </div>
    
    <!-- Search and Filters -->
    <div class="search-filters">
        @include('admin.customers.partials.search-form')
        @include('admin.customers.partials.advanced-filters')
    </div>
    
    <!-- Customer List -->
    <div class="customer-list">
        @include('admin.customers.partials.customer-table')
    </div>
    
    <!-- Bulk Actions -->
    <div class="bulk-actions" style="display: none;">
        @include('admin.customers.partials.bulk-actions')
    </div>
</div>

<!-- Customer Detail Modal -->
@include('admin.customers.modals.customer-detail')

<!-- Add/Edit Customer Modal -->
@include('admin.customers.modals.customer-form')
@endsection
```

### 8.3 JavaScript Functionality
```javascript
// Customer Management JavaScript
$(document).ready(function() {
    // Search functionality
    $('#search-form').on('submit', function(e) {
        e.preventDefault();
        performSearch();
    });
    
    // Row selection for bulk actions
    $('.customer-checkbox').on('change', function() {
        updateBulkActions();
    });
    
    // Bulk action handlers
    $('.bulk-action-btn').on('click', function() {
        const action = $(this).data('action');
        performBulkAction(action);
    });
    
    // Customer detail modal
    $('.view-customer-btn').on('click', function() {
        const customerId = $(this).data('customer-id');
        loadCustomerDetail(customerId);
    });
    
    // Auto-save form data
    $('.customer-form input, .customer-form textarea').on('blur', function() {
        autoSaveFormData();
    });
});

function performSearch() {
    const searchTerm = $('#search-input').val();
    const filters = getActiveFilters();
    
    $.ajax({
        url: '{{ route("customers.index") }}',
        method: 'GET',
        data: {
            search: searchTerm,
            ...filters
        },
        success: function(data) {
            updateCustomerTable(data);
        }
    });
}

function updateBulkActions() {
    const selectedCount = $('.customer-checkbox:checked').length;
    
    if (selectedCount > 0) {
        $('.bulk-actions').show();
        $('.selected-count').text(selectedCount);
    } else {
        $('.bulk-actions').hide();
    }
}

function loadCustomerDetail(customerId) {
    $.ajax({
        url: `/admin/customers/${customerId}`,
        method: 'GET',
        success: function(data) {
            $('#customer-detail-modal .modal-body').html(data);
            $('#customer-detail-modal').modal('show');
        }
    });
}
```

## 9. PERFORMANCE OPTIMIZATION

### 9.1 Database Optimization
```php
// Efficient queries with proper relationships
$customers = Customer::with([
    'invoices' => function($query) {
        $query->latest()->limit(1);
    },
    'meterReadings' => function($query) {
        $query->latest()->limit(1);
    }
])->paginate(20);
```

### 9.2 Caching Strategy
```php
// Cache customer list for 5 minutes
$customers = Cache::remember('customers_list_' . md5(serialize($request->all())), 300, function() use ($request) {
    return $this->getFilteredCustomers($request);
});
```

### 9.3 Lazy Loading
- Load customer details on demand
- Paginate large customer lists
- Use AJAX for search and filtering

## 10. SECURITY CONSIDERATIONS

### 10.1 Input Validation
- Sanitize all user inputs
- Validate file uploads
- Prevent SQL injection

### 10.2 Access Control
- Verify admin authentication
- Check permissions for sensitive operations
- Log all customer data modifications

### 10.3 Data Protection
- Encrypt sensitive customer data
- Implement proper session management
- Use HTTPS for all communications

This comprehensive plan provides a robust foundation for implementing a powerful customer management system that handles all aspects of customer lifecycle management efficiently and securely.