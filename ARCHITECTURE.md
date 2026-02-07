# Stores Manager Architecture

## Overview

This project follows a simplified Three-Layer Architecture designed for maintainability, scalability, and team collaboration.

The goal is to separate responsibilities between UI, Business Logic, and Data Access layers.

---

## Architecture Layers

### 1. UI Layer (Presentation Layer)

Responsible for:

- User Interaction
- Data Display
- Sending User Input to BLL

Rules:

- UI MUST NOT access DAL directly
- UI MUST NOT contain SQL
- UI MUST NOT contain Business Logic

UI communicates only with BLL.

---

### 2. BLL Layer (Business Logic Layer)

Responsible for:

- Validation
- Business Rules
- Managing Application Logic
- Calling DAL

BLL uses DTO objects to exchange data.

---

### 3. DAL Layer (Data Access Layer)

Responsible for:

- Database Communication
- Executing SQL Commands
- Returning DTO objects

Rules:

- DAL MUST NOT contain Business Logic
- DAL MUST NOT contain UI Logic

---

### 4. DTO Layer

DTOs are used to transfer data between layers.

Rules:

- DTO contains properties only
- DTO must NOT contain methods or validation

---

## Data Flow

UI → BLL → DAL → Database  
Database → DAL → BLL → UI

---

## Module Structure

Each module should contain:

- DTO
- DAL
- BLL
- UI Forms
- User Controls (Optional)

Example:

Items
- ItemDTO
- clsItemsData
- clsItem
- frmItemsScreen
- frmAddUPdateItem
- frmEditItem
- ctrItemCard (Optional)

---

## Naming Conventions

### Classes
clsItem  
clsItemsData  
ItemDTO  

### Forms
frmItemsScreen  
frmAddUpdateItem
frmEditItem

### User Controls
ctrItemCard  

---

## Business Rules Location

All validation and business rules MUST be inside BLL.

---

## Database Rules

- All tables must have Primary Key
- Use NVARCHAR instead of VARCHAR
- Use Default Constraints when possible
- Use Check Constraints for numeric values (Required), dates and length of strings (Optional)
- Use IsActive instead of deleting records

---

## Logging

All errors must be logged using Logging Layer from Company.Framework.

---

## Framework Usage

Company.Framework contains shared reusable components and should not be modified without approval.

---

## Future Improvements

- Advanced Reporting
- Performance Optimization
