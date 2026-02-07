# Contributing Guidelines

Thank you for contributing to Stores Manager Project.

---

## Branch Strategy

Do NOT commit directly to main branch.

Each feature must have its own branch.

Example:

feature/Categories-Management  
feature/user-management  

---

## Workflow

1. Pull latest code
2. Create new branch
3. Implement feature
4. Test your code
5. Create Pull Request

---

## Pull Request Rules

- Provide clear description
- Attach screenshots if UI changed
- Ensure project builds successfully

---

## Code Review

Each Pull Request must be reviewed by at least one team member.

Framework changes require approval from project maintainer.

---

## Coding Rules

- Follow Naming Conventions
- Keep methods small and readable
- Avoid duplicate code
- Add comments only when necessary

---

## Layer Rules

UI cannot access DAL directly.

All database operations must go through BLL.

---

## Commit Message Format

Use clear messages:

Add Categories Screen  
Fix User Validation Bug  
Improve Logging

---

## Testing

Before submitting Pull Request:

- Build solution
- Test feature manually
- Ensure no runtime errors

---

## Communication

Discuss major changes before implementation.

---

## Respect Architecture

Breaking architecture rules is not allowed.
