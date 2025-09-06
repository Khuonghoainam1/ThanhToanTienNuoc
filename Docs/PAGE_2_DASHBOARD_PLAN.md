# PAGE 2: ADMIN DASHBOARD - DETAILED PLAN

## Overview
The Admin Dashboard serves as the central command center for water service administrators, providing at-a-glance insights into system performance, key metrics, and quick access to essential functions.

## Page Information
- **Route**: `/admin/dashboard`
- **Controller**: `AdminController@dashboard`
- **View**: `admin/dashboard/index.blade.php`
- **Access Level**: Admin users only
- **Layout**: Main admin layout with sidebar navigation

## 1. PAGE STRUCTURE & LAYOUT

### 1.1 Header Section
```
┌─────────────────────────────────────────────────────────────┐
│ [Logo] Water Service Management    [User Menu] [Logout]     │
└─────────────────────────────────────────────────────────────┘
```

### 1.2 Main Content Area
```
┌─────────────────────────────────────────────────────────────┐
│ Sidebar │                Dashboard Content                  │
│         │                                                  │
│ [Menu]  │  ┌─────────────────────────────────────────────┐  │
│         │  │              Welcome Message                │  │
│         │  └─────────────────────────────────────────────┘  │
│         │                                                  │
│         │  ┌─────────┐ ┌─────────┐ ┌─────────┐ ┌─────────┐ │
│         │  │   KPI   │ │   KPI   │ │   KPI   │ │   KPI   │ │
│         │  │  Card 1 │ │  Card 2 │ │  Card 3 │ │  Card 4 │ │
│         │  └─────────┘ └─────────┘ └─────────┘ └─────────┘ │
│         │                                                  │
│         │  ┌─────────────────────────────────────────────┐  │
│         │  │            Quick Actions                    │  │
│         │  └─────────────────────────────────────────────┘  │
│         │                                                  │
│         │  ┌─────────────────┐ ┌─────────────────────────┐  │
│         │  │  Recent         │ │    System               │  │
│         │  │  Activity       │ │    Alerts               │  │
│         │  └─────────────────┘ └─────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
```

## 2. KEY PERFORMANCE INDICATORS (KPI CARDS)

### 2.1 KPI Card 1: Unpaid Households
```php
// Data Source: Invoice model with payment_status = 'unpaid'
$unpaidCount = Invoice::where('payment_status', 'unpaid')
    ->where('period', $currentPeriod)
    ->count();
```

**Display Elements:**
- **Icon**: Warning triangle (⚠️) or red circle
- **Title**: "Unpaid Households"
- **Value**: Number of unpaid customers
- **Color**: Red (#dc3545) for urgency
- **Action**: Click to view outstanding debt list
- **Trend**: Compare with previous period

### 2.2 KPI Card 2: Total Revenue
```php
// Data Source: Invoice model with payment_status = 'paid'
$totalRevenue = Invoice::where('payment_status', 'paid')
    ->where('period', $currentPeriod)
    ->sum('total_amount');
```

**Display Elements:**
- **Icon**: Money bill (💰) or green circle
- **Title**: "Total Revenue"
- **Value**: Formatted currency (VNĐ)
- **Color**: Green (#28a745) for positive
- **Action**: Click to view revenue report
- **Trend**: Percentage change from previous period

### 2.3 KPI Card 3: Water Consumption
```php
// Data Source: MeterReading model for current period
$totalConsumption = MeterReading::where('period', $currentPeriod)
    ->sum('consumption');
```

**Display Elements:**
- **Icon**: Water drop (💧) or blue circle
- **Title**: "Total Consumption"
- **Value**: Volume in m³
- **Color**: Blue (#007bff) for water theme
- **Action**: Click to view consumption report
- **Trend**: Compare with previous periods

### 2.4 KPI Card 4: Active Customers
```php
// Data Source: Customer model with status = 'active'
$activeCustomers = Customer::where('status', 'active')->count();
```

**Display Elements:**
- **Icon**: Users (👥) or purple circle
- **Title**: "Active Customers"
- **Value**: Total number
- **Color**: Purple (#6f42c1) for customer focus
- **Action**: Click to view customer list
- **Trend**: New registrations this month

## 3. QUICK ACTION BUTTONS

### 3.1 Primary Actions
```
┌─────────────────────────────────────────────────────────────┐
│  ┌─────────────────┐  ┌─────────────────┐  ┌──────────────┐  │
│  │   📊 New Meter  │  │   👤 Add        │  │   📋 Generate│  │
│  │   Reading       │  │   Customer      │  │   Reports    │  │
│  └─────────────────┘  └─────────────────┘  └──────────────┘  │
│                                                             │
│  ┌─────────────────┐  ┌─────────────────┐  ┌──────────────┐  │
│  │   🔔 Create     │  │   💰 Payment    │  │   📈 View    │  │
│  │   Notification  │  │   Management    │  │   Analytics  │  │
│  └─────────────────┘  └─────────────────┘  └──────────────┘  │
└─────────────────────────────────────────────────────────────┘
```

### 3.2 Action Button Specifications
- **Size**: Large buttons (200px width, 60px height)
- **Icons**: Font Awesome or similar icon library
- **Colors**: Primary theme colors with hover effects
- **Links**: Direct navigation to respective pages
- **Responsive**: Stack vertically on mobile devices

## 4. RECENT ACTIVITY FEED

### 4.1 Activity Types
```php
// Recent Activities Data Structure
$activities = [
    'meter_readings' => MeterReading::latest()->take(5)->get(),
    'payments' => Payment::latest()->take(5)->get(),
    'customers' => Customer::latest()->take(5)->get(),
    'notifications' => Notification::latest()->take(3)->get()
];
```

### 4.2 Activity Display Format
```
┌─────────────────────────────────────────────────────────────┐
│                    Recent Activity                          │
├─────────────────────────────────────────────────────────────┤
│ 🕐 2 hours ago                                              │
│ 📊 Meter reading completed for 150 customers (Jan 2024)    │
│                                                             │
│ 🕐 4 hours ago                                              │
│ 💰 Payment received from Customer #12345 (₫250,000)        │
│                                                             │
│ 🕐 1 day ago                                                │
│ 👤 New customer registered: Nguyen Van A                    │
│                                                             │
│ 🕐 2 days ago                                               │
│ 🔔 Notification published: "Water service interruption"     │
└─────────────────────────────────────────────────────────────┘
```

### 4.3 Activity Features
- **Real-time Updates**: AJAX refresh every 5 minutes
- **Click Actions**: Navigate to relevant detail pages
- **Time Formatting**: Relative time (2 hours ago, 1 day ago)
- **Icons**: Contextual icons for different activity types
- **Pagination**: Load more activities on scroll

## 5. SYSTEM ALERTS & NOTIFICATIONS

### 5.1 Alert Types
```php
// System Alerts Configuration
$alerts = [
    'overdue_payments' => $this->getOverduePayments(),
    'low_consumption' => $this->getLowConsumptionCustomers(),
    'system_errors' => $this->getSystemErrors(),
    'maintenance_required' => $this->getMaintenanceAlerts()
];
```

### 5.2 Alert Display
```
┌─────────────────────────────────────────────────────────────┐
│                    System Alerts                            │
├─────────────────────────────────────────────────────────────┤
│ ⚠️  HIGH PRIORITY                                           │
│ 15 customers have overdue payments (>30 days)              │
│ [View Details] [Send Reminders]                             │
│                                                             │
│ ℹ️  INFORMATION                                             │
│ 3 customers showing unusually low consumption               │
│ [Investigate] [Contact Customers]                           │
│                                                             │
│ ✅  SYSTEM STATUS                                           │
│ All systems operational - Last backup: 2 hours ago         │
└─────────────────────────────────────────────────────────────┘
```

## 6. RESPONSIVE DESIGN SPECIFICATIONS

### 6.1 Desktop Layout (≥1200px)
- **Grid System**: 4-column KPI cards
- **Sidebar**: Fixed 250px width
- **Content**: Flexible width with max 1200px
- **Quick Actions**: 3x2 grid layout

### 6.2 Tablet Layout (768px - 1199px)
- **Grid System**: 2-column KPI cards
- **Sidebar**: Collapsible sidebar
- **Content**: Full width
- **Quick Actions**: 2x3 grid layout

### 6.3 Mobile Layout (<768px)
- **Grid System**: 1-column KPI cards
- **Sidebar**: Hamburger menu
- **Content**: Full width with padding
- **Quick Actions**: 1-column stack

## 7. TECHNICAL IMPLEMENTATION

### 7.1 Controller Method
```php
public function dashboard()
{
    $currentPeriod = date('Y-m');
    $previousPeriod = date('Y-m', strtotime('-1 month'));
    
    // KPI Data
    $kpis = [
        'unpaid_households' => $this->getUnpaidHouseholds($currentPeriod),
        'total_revenue' => $this->getTotalRevenue($currentPeriod),
        'water_consumption' => $this->getWaterConsumption($currentPeriod),
        'active_customers' => $this->getActiveCustomers()
    ];
    
    // Recent Activities
    $activities = $this->getRecentActivities();
    
    // System Alerts
    $alerts = $this->getSystemAlerts();
    
    return view('admin.dashboard.index', compact('kpis', 'activities', 'alerts'));
}
```

### 7.2 Blade Template Structure
```php
@extends('admin.layouts.main')

@section('title', 'Dashboard')

@section('content')
<div class="dashboard-container">
    <!-- Welcome Message -->
    <div class="welcome-section">
        <h1>Welcome back, {{ auth('admin')->user()->name }}</h1>
        <p>Here's what's happening with your water service today.</p>
    </div>
    
    <!-- KPI Cards -->
    <div class="kpi-grid">
        @foreach($kpis as $key => $kpi)
            @include('admin.dashboard.components.kpi-card', ['kpi' => $kpi])
        @endforeach
    </div>
    
    <!-- Quick Actions -->
    <div class="quick-actions">
        @include('admin.dashboard.components.quick-actions')
    </div>
    
    <!-- Activity Feed & Alerts -->
    <div class="dashboard-bottom">
        <div class="activity-feed">
            @include('admin.dashboard.components.activity-feed', ['activities' => $activities])
        </div>
        <div class="system-alerts">
            @include('admin.dashboard.components.system-alerts', ['alerts' => $alerts])
        </div>
    </div>
</div>
@endsection
```

### 7.3 JavaScript Functionality
```javascript
// Auto-refresh dashboard data
setInterval(function() {
    refreshDashboardData();
}, 300000); // 5 minutes

// KPI card click handlers
$('.kpi-card').on('click', function() {
    const action = $(this).data('action');
    window.location.href = action;
});

// Real-time activity updates
function refreshDashboardData() {
    $.ajax({
        url: '/admin/dashboard/refresh',
        method: 'GET',
        success: function(data) {
            updateKPICards(data.kpis);
            updateActivityFeed(data.activities);
            updateSystemAlerts(data.alerts);
        }
    });
}
```

## 8. PERFORMANCE CONSIDERATIONS

### 8.1 Database Optimization
- **Indexing**: Ensure proper indexes on frequently queried fields
- **Caching**: Cache KPI calculations for 5 minutes
- **Query Optimization**: Use efficient queries with proper joins

### 8.2 Frontend Optimization
- **Lazy Loading**: Load activity feed on scroll
- **Image Optimization**: Optimize icons and graphics
- **CSS/JS Minification**: Minify assets for faster loading

### 8.3 Caching Strategy
```php
// Cache KPI data for 5 minutes
$kpis = Cache::remember('dashboard_kpis_' . $currentPeriod, 300, function() use ($currentPeriod) {
    return $this->calculateKPIs($currentPeriod);
});
```

## 9. ACCESSIBILITY FEATURES

### 9.1 ARIA Labels
- All interactive elements have proper ARIA labels
- Screen reader friendly navigation
- Keyboard navigation support

### 9.2 Color Contrast
- WCAG AA compliant color contrast ratios
- Color-blind friendly color schemes
- Alternative indicators beyond color

### 9.3 Mobile Accessibility
- Touch-friendly button sizes (minimum 44px)
- Proper viewport configuration
- Responsive text sizing

## 10. SECURITY CONSIDERATIONS

### 10.1 Data Protection
- Sanitize all displayed data
- Prevent XSS attacks
- Secure API endpoints

### 10.2 Access Control
- Verify admin authentication
- Check permissions for sensitive data
- Log dashboard access

This comprehensive plan provides a solid foundation for implementing a powerful and user-friendly admin dashboard that serves as the central hub for water service management operations.