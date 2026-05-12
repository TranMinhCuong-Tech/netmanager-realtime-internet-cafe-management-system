# Commit Rules

## Format

```text
type(scope): short message
```

## Types

- `feat`: add new feature
- `fix`: resolve bug
- `refactor`: clean up code without changing behavior
- `docs`: documentation only
- `test`: add or update tests
- `chore`: maintenance task

## Scopes

Use only these scopes:

- `chat`
- `server`
- `network`
- `gui`
- `database`
- `lock`
- `docs`
- `test`
- `auth`
- `shared`

## Rules

- Use lowercase `type` and `scope`.
- Keep the message short and clear.
- One commit should focus on one main change.
- Write in English.
- Split large changes into smaller commits.

## Examples

- `feat(chat): add realtime messaging`
- `fix(server): resolve timeout issue`
- `feat(gui): create dashboard form`
- `fix(network): handle reconnect`
- `docs(architecture): update system overview`
- `feat(database): add user login`
- `test(shared): add packet parser tests`
