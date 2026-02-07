# Coding Standards

---

## General Rules

- Write clean readable code
- Use meaningful names
- Avoid magic numbers
- Avoid deep nesting

---

## Naming Conventions

### Classes
PascalCase

Example:
clsItem
clsUser

---

### Methods
PascalCase

Example:
GetItems
SaveUser

---

### Variables
camelCase

Example:
itemName
storeID

---

### Private Fields
_startWithUnderscore

Example:
_mode
_currentUser

---

## DTO Rules

DTO contains properties only.

No logic inside DTO.

---

## DAL Rules

- Only database operations
- Use parameterized queries
- Use using blocks for connections

---

## BLL Rules

- Contains validation
- Contains business rules
- Calls DAL only

---

## UI Rules

- No SQL inside Forms
- No business rules inside UI
- UI communicates only with BLL

---

## Exception Handling

Always log exceptions using Logging Layer.

---

## Validation

All validation must be inside BLL.

---

## Comments

Write comments only if code is not self-explanatory.

---

## Method Length

Keep methods short and focused.

---

## Reusability

If code repeats more than twice, consider moving it to Framework.
