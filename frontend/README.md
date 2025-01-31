# Frågor att svara på

- Vad är vite? Hur funkar det?
- Vad är Tanstack Query, hur funkar det? När ska man använda det?
- Vad är Tanstack Router, hur funkar det? När ska man använda det?
- Vad är Shadcn/ui, hur funkar det? När ska man använda det?

- XP comparison över tid.

# Vite

# Tanstack Query

# Tanstack Router

Showcase

- Skapa nya routes.

# Shadcn

Important to note is that shadcn requires tailwind. If this is undesirable then you'll either have to reconfigure the css yourself, however this loses a lot of the positives of shadcn, so then perhaps look of using another component library.

Shadcn bygger på ett annat komponent bibliotek som heter radix, så även fast man inte har ett direkt dependency till shadcnui så har man fortfarande ett dependency till radix.

## Installation

See the installation guide on Shadcnui https://ui.shadcn.com/docs/installation/vite.

# Tailwind

# React + TypeScript + Vite

This template provides a minimal setup to get React working in Vite with HMR and some ESLint rules.

Currently, two official plugins are available:

- [@vitejs/plugin-react](https://github.com/vitejs/vite-plugin-react/blob/main/packages/plugin-react/README.md) uses [Babel](https://babeljs.io/) for Fast Refresh
- [@vitejs/plugin-react-swc](https://github.com/vitejs/vite-plugin-react-swc) uses [SWC](https://swc.rs/) for Fast Refresh

## Expanding the ESLint configuration

If you are developing a production application, we recommend updating the configuration to enable type aware lint rules:

- Configure the top-level `parserOptions` property like this:

```js
export default tseslint.config({
  languageOptions: {
    // other options...
    parserOptions: {
      project: ["./tsconfig.node.json", "./tsconfig.app.json"],
      tsconfigRootDir: import.meta.dirname,
    },
  },
});
```

- Replace `tseslint.configs.recommended` to `tseslint.configs.recommendedTypeChecked` or `tseslint.configs.strictTypeChecked`
- Optionally add `...tseslint.configs.stylisticTypeChecked`
- Install [eslint-plugin-react](https://github.com/jsx-eslint/eslint-plugin-react) and update the config:

```js
// eslint.config.js
import react from "eslint-plugin-react";

export default tseslint.config({
  // Set the react version
  settings: { react: { version: "18.3" } },
  plugins: {
    // Add the react plugin
    react,
  },
  rules: {
    // other rules...
    // Enable its recommended rules
    ...react.configs.recommended.rules,
    ...react.configs["jsx-runtime"].rules,
  },
});
```
