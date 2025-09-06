# PAGE 4: METER READING & BILLING - DETAILED PLAN

## Overview
The Meter Reading & Billing page is the core operational interface for water service administrators to perform bulk meter reading data entry, manage pricing, and generate invoices for all customers in a billing period.

## Page Information
- **Route**: `/admin/billing/meter-reading`
- **Controller**: `BillingController@meterReading`
- **View**: `admin/billing/meter-reading.blade.php`
- **Access Level**: Admin users only
- **Layout**: Main admin layout with sidebar navigation

## 1. PAGE STRUCTURE & LAYOUT

### 1.1 Header Section
```
┌─────────────────────────────────────────────────────────────┐
│ Meter Reading & Billing              [Save] [Generate Bills] │
└─────────────────────────────────────────────────────────────┘
```

### 1.2 Main Content Area
```
┌─────────────────────────────────────────────────────────────┐
│ Sidebar │                Meter Reading & Billing            │
│         │                                                  │
│ [Menu]  │  ┌─────────────────────────────────────────────┐  │
│         │  │              Period & Pricing               │  │
│         │  └─────────────────────────────────────────────┘  │
│         │                                                  │
│         │  ┌─────────────────────────────────────────────┐  │
│         │  │              Bulk Meter Reading             │  │
│         │  │              Form                           │  │
│         │  └─────────────────────────────────────────────┘  │
│         │                                                  │
│         │  ┌─────────────────────────────────────────────┐  │
│         │  │              Preview & Actions              │  │
│         │  └─────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────┘
```

## 2. PERIOD & PRICING CONFIGURATION

### 2.1 Billing Period Selection
```php
// Period Selection Interface
$periodOptions = [
    'current_month' => date('Y-m'),
    'previous_month' => date('Y-m', strtotime('-1 month')),
    'custom' => 'Custom Period'
];

$billingPeriods = [
    'monthly' => 'Monthly Billing',
    'quarterly' => 'Quarterly Billing'
];
```

### 2.2 Period Selection Interface
```
┌─────────────────────────────────────────────────────────────┐
│                    Billing Period Setup                     │
├─────────────────────────────────────────────────────────────┤
│ Billing Period: [Monthly ▼]                                │
│                                                             │
│ Period Selection:                                           │
│ ○ Current Month (2024-01)                                  │
│ ○ Previous Month (2023-12)                                 │
│ ○ Custom Period: [2024-01] to [2024-01]                    │
│                                                             │
│ Unit Price (VNĐ/m³): [5,000] [Set Default]                 │
│                                                             │
│ Due Date: [2024-02-15] (15 days after billing)             │
│                                                             │
│ [Load Customer List] [Reset]                               │
└─────────────────────────────────────────────────────────────┘
```

### 2.3 Pricing Management
```php
// Pricing Configuration
$pricingConfig = [
    'base_rate' => 5000, // VNĐ per m³
    'tier_rates' => [
        'tier_1' => ['min' => 0, 'max' => 10, 'rate' => 4000],
        'tier_2' => ['min' => 11, 'max' => 20, 'rate' => 5000],
        'tier_3' => ['min' => 21, 'max' => 50, 'rate' => 6000],
        'tier_4' => ['min' => 51, 'max' => null, 'rate' => 8000]
    ],
    'fixed_charges' => [
        'connection_fee' => 50000,
        'service_fee' => 10000
    ]
];
```

## 3. BULK METER READING FORM

### 3.1 Customer List with Meter Reading Inputs
```php
// Customer Data Structure for Meter Reading
$customers = Customer::where('status', 'active')
    ->with(['meterReadings' => function($query) use ($period) {
        $query->where('period', $period)->latest();
    }])
    ->orderBy('customer_code')
    ->get();

foreach ($customers as $customer) {
    $lastReading = $customer->meterReadings->first();
    $customer->previous_reading = $lastReading ? $lastReading->current_reading : 0;
    $customer->current_reading = null; // To be filled by admin
}
```

### 3.2 Bulk Reading Form Layout
```
┌─────────────────────────────────────────────────────────────┐
│                    Bulk Meter Reading Form                  │
├─────────────────────────────────────────────────────────────┤
│ Period: 2024-01 | Unit Price: ₫5,000/m³ | Customers: 150   │
│                                                             │
│ ┌─────────────────────────────────────────────────────────┐ │
│ │ Customer ID │ Name           │ Previous │ Current │ Diff │ │
│ ├─────────────┼────────────────┼──────────┼─────────┼──────┤ │
│ │ C001234     │ Nguyen Van A   │ 1,250    │ [1,265] │ 15   │ │
│ │ C001235     │ Tran Thi B     │ 980      │ [995]   │ 15   │ │
│ │ C001236     │ Le Van C       │ 1,100    │ [1,115] │ 15   │ │
│ │ C001237     │ Pham Thi D     │ 750      │ [765]   │ 15   │ │
│ │ ...         │ ...            │ ...      │ ...     │ ...  │ │
│ └─────────────┴────────────────┴──────────┴─────────┴──────┘ │
│                                                             │
│ [Save Readings] [Validate All] [Clear All] [Import CSV]     │
└─────────────────────────────────────────────────────────────┘
```

### 3.3 Form Features
- **Auto-calculation**: Consumption automatically calculated as current - previous
- **Validation**: Real-time validation for reading accuracy
- **Bulk Operations**: Select all, clear all, validate all
- **Import/Export**: CSV import for bulk data entry
- **Search/Filter**: Quick customer search within the form
- **Progress Indicator**: Show completion percentage

## 4. METER READING VALIDATION

### 4.1 Validation Rules
```php
// Meter Reading Validation
$validationRules = [
    'current_reading' => [
        'required',
        'numeric',
        'min:0',
        'gte:previous_reading', // Current must be >= previous
        'max:999999' // Reasonable upper limit
    ],
    'consumption' => [
        'required',
        'numeric',
        'min:0',
        'max:1000' // Reasonable consumption limit
    ]
];

// Business Logic Validation
$businessRules = [
    'unusual_consumption' => 'Consumption > 3x average for customer',
    'negative_consumption' => 'Current reading < previous reading',
    'zero_consumption' => 'No consumption for 3+ consecutive periods',
    'excessive_consumption' => 'Consumption > 100 m³ per month'
];
```

### 4.2 Validation Interface
```
┌─────────────────────────────────────────────────────────────┐
│                    Validation Results                       │
├─────────────────────────────────────────────────────────────┤
│ ✅ Valid Readings: 145 customers                           │
│ ⚠️  Warnings: 3 customers                                 │
│ ❌ Errors: 2 customers                                     │
│                                                             │
│ ┌─────────────────────────────────────────────────────────┐ │
│ │ Warnings:                                               │ │
│ │ • C001234: High consumption (45 m³ vs avg 15 m³)       │ │
│ │ • C001235: Zero consumption for 2nd month              │ │
│ │ • C001236: Reading unchanged from last month           │ │
│ └─────────────────────────────────────────────────────────┘ │
│                                                             │
│ ┌─────────────────────────────────────────────────────────┐ │
│ │ Errors:                                                 │ │
│ │ • C001237: Current reading (950) < Previous (980)      │ │
│ │ • C001238: Invalid reading format                       │ │
│ └─────────────────────────────────────────────────────────┘ │
│                                                             │
│ [Fix Errors] [Override Warnings] [Proceed Anyway]          │
└─────────────────────────────────────────────────────────────┘
```

## 5. INVOICE GENERATION & PREVIEW

### 5.1 Invoice Calculation
```php
// Invoice Calculation Logic
class InvoiceCalculator
{
    public function calculateInvoice($customer, $consumption, $unitPrice)
    {
        $baseAmount = $consumption * $unitPrice;
        
        // Apply tiered pricing if configured
        $tieredAmount = $this->calculateTieredPricing($consumption);
        
        // Add fixed charges
        $fixedCharges = $this->getFixedCharges();
        
        $totalAmount = $tieredAmount + $fixedCharges;
        
        return [
            'consumption' => $consumption,
            'unit_price' => $unitPrice,
            'base_amount' => $baseAmount,
            'tiered_amount' => $tieredAmount,
            'fixed_charges' => $fixedCharges,
            'total_amount' => $totalAmount,
            'due_date' => $this->calculateDueDate()
        ];
    }
    
    private function calculateTieredPricing($consumption)
    {
        $total = 0;
        $remaining = $consumption;
        
        foreach ($this->tierRates as $tier) {
            if ($remaining <= 0) break;
            
            $tierConsumption = min($remaining, $tier['max'] - $tier['min'] + 1);
            $total += $tierConsumption * $tier['rate'];
            $remaining -= $tierConsumption;
        }
        
        return $total;
    }
}
```

### 5.2 Invoice Preview
```
┌─────────────────────────────────────────────────────────────┐
│                    Invoice Preview                          │
├─────────────────────────────────────────────────────────────┤
│ Period: 2024-01 | Total Customers: 150 | Total Amount: ₫2.5M │
│                                                             │
│ ┌─────────────────────────────────────────────────────────┐ │
│ │ Customer ID │ Name           │ Consumption │ Amount     │ │
│ ├─────────────┼────────────────┼─────────────┼────────────┤ │
│ │ C001234     │ Nguyen Van A   │ 15 m³       │ ₫75,000    │ │
│ │ C001235     │ Tran Thi B     │ 12 m³       │ ₫60,000    │ │
│ │ C001236     │ Le Van C       │ 18 m³       │ ₫90,000    │ │
│ │ C001237     │ Pham Thi D     │ 8 m³        │ ₫40,000    │ │
│ │ ...         │ ...            │ ...         │ ...        │ │
│ └─────────────┴────────────────┴─────────────┴────────────┘ │
│                                                             │
│ Summary:                                                    │
│ • Total Consumption: 2,250 m³                              │
│ • Average Consumption: 15 m³ per customer                  │
│ • Total Revenue: ₫11,250,000                               │
│ • Due Date: February 15, 2024                              │
│                                                             │
│ [Generate Invoices] [Export Preview] [Modify Pricing]       │
└─────────────────────────────────────────────────────────────┘
```

## 6. BULK OPERATIONS & AUTOMATION

### 6.1 Bulk Reading Operations
```php
// Bulk Operations Available
$bulkOperations = [
    'save_readings' => 'Save All Readings',
    'validate_all' => 'Validate All Readings',
    'clear_all' => 'Clear All Readings',
    'import_csv' => 'Import from CSV',
    'export_template' => 'Export Template',
    'generate_invoices' => 'Generate All Invoices',
    'send_notifications' => 'Send Billing Notifications'
];
```

### 6.2 CSV Import/Export
```php
// CSV Import Structure
$csvStructure = [
    'headers' => ['customer_code', 'current_reading', 'notes'],
    'sample_data' => [
        ['C001234', '1265', 'Normal reading'],
        ['C001235', '995', 'Customer present'],
        ['C001236', '1115', 'Estimated reading']
    ]
];

// CSV Export Template
public function exportTemplate()
{
    $customers = Customer::where('status', 'active')
        ->select('customer_code', 'household_head_name', 'previous_reading')
        ->get();
    
    $filename = 'meter_reading_template_' . date('Y-m-d') . '.csv';
    
    return response()->streamDownload(function() use ($customers) {
        $file = fopen('php://output', 'w');
        
        // Headers
        fputcsv($file, ['Customer Code', 'Customer Name', 'Previous Reading', 'Current Reading', 'Notes']);
        
        // Data
        foreach ($customers as $customer) {
            fputcsv($file, [
                $customer->customer_code,
                $customer->household_head_name,
                $customer->previous_reading,
                '', // Empty for current reading
                ''  // Empty for notes
            ]);
        }
        
        fclose($file);
    }, $filename);
}
```

## 7. RESPONSIVE DESIGN SPECIFICATIONS

### 7.1 Desktop Layout (≥1200px)
- **Form**: Full-width table with all columns visible
- **Actions**: Horizontal action bar
- **Preview**: Side-by-side preview panel
- **Validation**: Inline validation messages

### 7.2 Tablet Layout (768px - 1199px)
- **Form**: Horizontal scroll for table
- **Actions**: Collapsible action menu
- **Preview**: Tabbed preview interface
- **Validation**: Modal validation results

### 7.3 Mobile Layout (<768px)
- **Form**: Card-based layout per customer
- **Actions**: Bottom action sheet
- **Preview**: Full-screen preview
- **Validation**: Step-by-step validation

### 7.4 Mobile Card Layout
```
┌─────────────────────────────────────────────────────────────┐
│ Customer Card - C001234                                     │
├─────────────────────────────────────────────────────────────┤
│ Name: Nguyen Van A                                          │
│ Previous: 1,250 m³ | Current: [1,265] m³                   │
│ Consumption: 15 m³ | Amount: ₫75,000                       │
│                                                             │
│ [Validate] [Save] [Notes]                                   │
└─────────────────────────────────────────────────────────────┘
```

## 8. TECHNICAL IMPLEMENTATION

### 8.1 Controller Methods
```php
class BillingController extends Controller
{
    public function meterReading(Request $request)
    {
        $period = $request->get('period', date('Y-m'));
        $unitPrice = $request->get('unit_price', 5000);
        
        $customers = Customer::where('status', 'active')
            ->with(['meterReadings' => function($query) use ($period) {
                $query->where('period', $period)->latest();
            }])
            ->orderBy('customer_code')
            ->get();
        
        // Prepare customer data for form
        $customers = $customers->map(function($customer) use ($period) {
            $lastReading = $customer->meterReadings->first();
            $customer->previous_reading = $lastReading ? $lastReading->current_reading : 0;
            $customer->current_reading = null;
            return $customer;
        });
        
        return view('admin.billing.meter-reading', compact('customers', 'period', 'unitPrice'));
    }
    
    public function storeMeterReading(Request $request)
    {
        $validated = $request->validate([
            'period' => 'required|date_format:Y-m',
            'unit_price' => 'required|numeric|min:0',
            'readings' => 'required|array',
            'readings.*.customer_id' => 'required|exists:customers,id',
            'readings.*.current_reading' => 'required|numeric|min:0',
            'readings.*.notes' => 'nullable|string|max:500'
        ]);
        
        $period = $validated['period'];
        $unitPrice = $validated['unit_price'];
        $readings = $validated['readings'];
        
        DB::beginTransaction();
        
        try {
            $meterReadings = [];
            $invoices = [];
            
            foreach ($readings as $reading) {
                $customer = Customer::find($reading['customer_id']);
                $lastReading = $customer->meterReadings()
                    ->where('period', '<', $period)
                    ->latest()
                    ->first();
                
                $previousReading = $lastReading ? $lastReading->current_reading : 0;
                $consumption = $reading['current_reading'] - $previousReading;
                $totalAmount = $consumption * $unitPrice;
                
                // Create meter reading record
                $meterReading = MeterReading::create([
                    'customer_id' => $customer->id,
                    'period' => $period,
                    'previous_reading' => $previousReading,
                    'current_reading' => $reading['current_reading'],
                    'consumption' => $consumption,
                    'unit_price' => $unitPrice,
                    'total_amount' => $totalAmount,
                    'reading_date' => now(),
                    'created_by' => auth('admin')->id(),
                    'notes' => $reading['notes'] ?? null
                ]);
                
                // Create invoice record
                $invoice = Invoice::create([
                    'customer_id' => $customer->id,
                    'meter_reading_id' => $meterReading->id,
                    'period' => $period,
                    'amount' => $totalAmount,
                    'payment_status' => 'unpaid',
                    'due_date' => now()->addDays(15),
                    'created_at' => now()
                ]);
                
                $meterReadings[] = $meterReading;
                $invoices[] = $invoice;
            }
            
            DB::commit();
            
            return response()->json([
                'success' => true,
                'message' => 'Meter readings saved successfully',
                'data' => [
                    'meter_readings_count' => count($meterReadings),
                    'invoices_count' => count($invoices),
                    'total_amount' => array_sum(array_column($invoices, 'amount'))
                ]
            ]);
            
        } catch (\Exception $e) {
            DB::rollback();
            
            return response()->json([
                'success' => false,
                'message' => 'Error saving meter readings: ' . $e->getMessage()
            ], 500);
        }
    }
    
    public function validateReadings(Request $request)
    {
        $readings = $request->readings;
        $validationResults = [
            'valid' => [],
            'warnings' => [],
            'errors' => []
        ];
        
        foreach ($readings as $reading) {
            $customer = Customer::find($reading['customer_id']);
            $lastReading = $customer->meterReadings()
                ->where('period', '<', $request->period)
                ->latest()
                ->first();
            
            $previousReading = $lastReading ? $lastReading->current_reading : 0;
            $consumption = $reading['current_reading'] - $previousReading;
            
            // Validation rules
            if ($reading['current_reading'] < $previousReading) {
                $validationResults['errors'][] = [
                    'customer_id' => $customer->id,
                    'customer_code' => $customer->customer_code,
                    'message' => 'Current reading cannot be less than previous reading'
                ];
            } elseif ($consumption > 100) {
                $validationResults['warnings'][] = [
                    'customer_id' => $customer->id,
                    'customer_code' => $customer->customer_code,
                    'message' => 'Unusually high consumption detected'
                ];
            } else {
                $validationResults['valid'][] = [
                    'customer_id' => $customer->id,
                    'customer_code' => $customer->customer_code,
                    'consumption' => $consumption
                ];
            }
        }
        
        return response()->json($validationResults);
    }
    
    public function generateInvoices(Request $request)
    {
        $period = $request->period;
        $unitPrice = $request->unit_price;
        
        $meterReadings = MeterReading::where('period', $period)
            ->where('unit_price', $unitPrice)
            ->get();
        
        $invoices = [];
        
        foreach ($meterReadings as $reading) {
            $invoice = Invoice::updateOrCreate([
                'customer_id' => $reading->customer_id,
                'period' => $period
            ], [
                'meter_reading_id' => $reading->id,
                'amount' => $reading->total_amount,
                'payment_status' => 'unpaid',
                'due_date' => now()->addDays(15)
            ]);
            
            $invoices[] = $invoice;
        }
        
        return response()->json([
            'success' => true,
            'message' => 'Invoices generated successfully',
            'invoices_count' => count($invoices),
            'total_amount' => array_sum(array_column($invoices, 'amount'))
        ]);
    }
}
```

### 8.2 Blade Template Structure
```php
@extends('admin.layouts.main')

@section('title', 'Meter Reading & Billing')

@section('content')
<div class="meter-reading-billing">
    <!-- Header -->
    <div class="page-header">
        <h1>Meter Reading & Billing</h1>
        <div class="header-actions">
            <button type="button" class="btn btn-secondary" id="save-readings">
                <i class="fas fa-save"></i> Save Readings
            </button>
            <button type="button" class="btn btn-primary" id="generate-invoices">
                <i class="fas fa-file-invoice"></i> Generate Invoices
            </button>
        </div>
    </div>
    
    <!-- Period & Pricing Configuration -->
    <div class="period-pricing-config">
        @include('admin.billing.partials.period-pricing')
    </div>
    
    <!-- Bulk Meter Reading Form -->
    <div class="meter-reading-form">
        @include('admin.billing.partials.bulk-reading-form')
    </div>
    
    <!-- Validation Results -->
    <div class="validation-results" style="display: none;">
        @include('admin.billing.partials.validation-results')
    </div>
    
    <!-- Invoice Preview -->
    <div class="invoice-preview" style="display: none;">
        @include('admin.billing.partials.invoice-preview')
    </div>
</div>

<!-- Bulk Operations Modal -->
@include('admin.billing.modals.bulk-operations')

<!-- CSV Import Modal -->
@include('admin.billing.modals.csv-import')
@endsection
```

### 8.3 JavaScript Functionality
```javascript
// Meter Reading & Billing JavaScript
$(document).ready(function() {
    // Auto-calculate consumption
    $('.current-reading-input').on('input', function() {
        calculateConsumption($(this));
    });
    
    // Bulk operations
    $('#save-readings').on('click', function() {
        saveMeterReadings();
    });
    
    $('#generate-invoices').on('click', function() {
        generateInvoices();
    });
    
    // Validation
    $('#validate-all').on('click', function() {
        validateAllReadings();
    });
    
    // CSV import/export
    $('#import-csv').on('click', function() {
        $('#csv-import-modal').modal('show');
    });
    
    $('#export-template').on('click', function() {
        exportTemplate();
    });
    
    // Real-time validation
    $('.current-reading-input').on('blur', function() {
        validateReading($(this));
    });
});

function calculateConsumption(input) {
    const row = input.closest('tr');
    const previousReading = parseFloat(row.find('.previous-reading').text()) || 0;
    const currentReading = parseFloat(input.val()) || 0;
    const consumption = currentReading - previousReading;
    
    row.find('.consumption-display').text(consumption.toFixed(1));
    
    // Update amount
    const unitPrice = parseFloat($('#unit-price').val()) || 0;
    const amount = consumption * unitPrice;
    row.find('.amount-display').text(formatCurrency(amount));
}

function validateReading(input) {
    const row = input.closest('tr');
    const customerId = row.data('customer-id');
    const currentReading = parseFloat(input.val()) || 0;
    const previousReading = parseFloat(row.find('.previous-reading').text()) || 0;
    
    // Basic validation
    if (currentReading < previousReading) {
        input.addClass('is-invalid');
        row.find('.validation-message').text('Current reading cannot be less than previous reading');
    } else if (currentReading === previousReading) {
        input.addClass('is-warning');
        row.find('.validation-message').text('No consumption detected');
    } else {
        input.removeClass('is-invalid is-warning');
        row.find('.validation-message').text('');
    }
}

function saveMeterReadings() {
    const readings = [];
    
    $('.meter-reading-row').each(function() {
        const row = $(this);
        const customerId = row.data('customer-id');
        const currentReading = parseFloat(row.find('.current-reading-input').val());
        const notes = row.find('.notes-input').val();
        
        if (currentReading !== null && !isNaN(currentReading)) {
            readings.push({
                customer_id: customerId,
                current_reading: currentReading,
                notes: notes
            });
        }
    });
    
    $.ajax({
        url: '{{ route("billing.store-meter-reading") }}',
        method: 'POST',
        data: {
            period: $('#period').val(),
            unit_price: $('#unit-price').val(),
            readings: readings,
            _token: $('meta[name="csrf-token"]').attr('content')
        },
        success: function(response) {
            if (response.success) {
                showSuccessMessage('Meter readings saved successfully');
                updateProgressBar();
            } else {
                showErrorMessage(response.message);
            }
        },
        error: function(xhr) {
            showErrorMessage('Error saving meter readings');
        }
    });
}

function validateAllReadings() {
    const readings = [];
    
    $('.meter-reading-row').each(function() {
        const row = $(this);
        const customerId = row.data('customer-id');
        const currentReading = parseFloat(row.find('.current-reading-input').val());
        
        if (currentReading !== null && !isNaN(currentReading)) {
            readings.push({
                customer_id: customerId,
                current_reading: currentReading
            });
        }
    });
    
    $.ajax({
        url: '{{ route("billing.validate-readings") }}',
        method: 'POST',
        data: {
            period: $('#period').val(),
            readings: readings,
            _token: $('meta[name="csrf-token"]').attr('content')
        },
        success: function(response) {
            displayValidationResults(response);
        }
    });
}

function displayValidationResults(results) {
    const validationDiv = $('.validation-results');
    
    // Clear previous results
    validationDiv.find('.validation-summary').remove();
    validationDiv.find('.validation-details').remove();
    
    // Add summary
    const summary = `
        <div class="validation-summary">
            <div class="alert alert-success">
                <i class="fas fa-check-circle"></i>
                Valid Readings: ${results.valid.length} customers
            </div>
            <div class="alert alert-warning">
                <i class="fas fa-exclamation-triangle"></i>
                Warnings: ${results.warnings.length} customers
            </div>
            <div class="alert alert-danger">
                <i class="fas fa-times-circle"></i>
                Errors: ${results.errors.length} customers
            </div>
        </div>
    `;
    
    validationDiv.append(summary);
    
    // Add details
    if (results.warnings.length > 0 || results.errors.length > 0) {
        const details = `
            <div class="validation-details">
                <h5>Validation Details</h5>
                ${results.warnings.map(w => `<div class="alert alert-warning">${w.customer_code}: ${w.message}</div>`).join('')}
                ${results.errors.map(e => `<div class="alert alert-danger">${e.customer_code}: ${e.message}</div>`).join('')}
            </div>
        `;
        
        validationDiv.append(details);
    }
    
    validationDiv.show();
}

function generateInvoices() {
    $.ajax({
        url: '{{ route("billing.generate-invoices") }}',
        method: 'POST',
        data: {
            period: $('#period').val(),
            unit_price: $('#unit-price').val(),
            _token: $('meta[name="csrf-token"]').attr('content')
        },
        success: function(response) {
            if (response.success) {
                showSuccessMessage(`Invoices generated successfully: ${response.invoices_count} invoices, Total: ${formatCurrency(response.total_amount)}`);
                showInvoicePreview(response);
            } else {
                showErrorMessage(response.message);
            }
        }
    });
}

function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(amount);
}

function showSuccessMessage(message) {
    toastr.success(message);
}

function showErrorMessage(message) {
    toastr.error(message);
}
```

## 9. PERFORMANCE OPTIMIZATION

### 9.1 Database Optimization
```php
// Efficient bulk operations
DB::transaction(function() use ($readings) {
    // Bulk insert meter readings
    MeterReading::insert($meterReadingData);
    
    // Bulk insert invoices
    Invoice::insert($invoiceData);
    
    // Update customer statistics
    $this->updateCustomerStatistics($period);
});
```

### 9.2 Frontend Optimization
- **Lazy Loading**: Load customer data in chunks
- **Debounced Input**: Delay validation on input
- **Virtual Scrolling**: For large customer lists
- **Progressive Enhancement**: Basic functionality without JavaScript

### 9.3 Caching Strategy
```php
// Cache customer list for current period
$customers = Cache::remember("customers_for_period_{$period}", 300, function() use ($period) {
    return $this->getCustomersForPeriod($period);
});
```

## 10. SECURITY CONSIDERATIONS

### 10.1 Data Validation
- **Input Sanitization**: Clean all user inputs
- **Business Logic Validation**: Enforce reading accuracy rules
- **CSRF Protection**: Token validation for all forms

### 10.2 Access Control
- **Admin Authentication**: Verify admin permissions
- **Audit Logging**: Log all meter reading changes
- **Data Integrity**: Prevent duplicate readings

### 10.3 Error Handling
- **Graceful Degradation**: Handle partial failures
- **User Feedback**: Clear error messages
- **Recovery Options**: Allow correction of errors

This comprehensive plan provides a robust foundation for implementing a powerful meter reading and billing system that handles bulk operations efficiently while maintaining data accuracy and user experience.