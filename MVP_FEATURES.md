# MVP Features – Stores Manager System

## Purpose
This document defines the Minimum Viable Product (MVP) that must be delivered to the client.

Only features listed here are required for first release.

Any additional feature is considered Future Enhancement.

---

# Core Modules

## 1. Categories Management

### Features
- Add New Category
- Edit Category
- Update Category
- Activate / Deactivate Category
- View Categories List
- Search Category by Code (CategoryID)

---

## 2. Stores Management

### Features
- Add Store
- Edit Store
- Update Store
- Activate / Deactivate Store
- View Stores List
- Search Store by Code (StoreID)
- View Store Details (including items and quantities)

---

## 3. Inventory Transactions

### Features

#### Add Quantity
- Add quantity to store
- Select Category
- Select Store
- Enter Quantity
- Save Transaction

---

#### Issue
- Issue quantity from store
- Prevent issuing more than available stock

---

#### Transfer Between Stores
- Transfer item from Store A to Store B
- Generate Transfer Code
- Save Transfer Transaction

---

## 4. Users Management

### Features
- Register Admin
- View Users List
- Add User
- Edit User
- Update User
- Activate / Deactivate User
- Login System

---

## 5. Permissions

### Features
- Assign Permissions to Users
- Restrict access to modules

---

## 6. Basic Reports

### Reports
- Categories within a specific warehouse
- Categories balance in a warehouse
- Total balance of an Category across all warehouses
- Transaction log with filters (Date – Category – Warehouse - Transaction Type)

---

# Out of MVP Scope (Future Features)

- Notifications
- Advanced Reports
- Performance Optimization
- Multi-Language Support
- Barcode Support
