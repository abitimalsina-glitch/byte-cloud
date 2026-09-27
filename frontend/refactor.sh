#!/bin/bash

# Ensure src directories exist
mkdir -p src/components
mkdir -p src/pages

# Move globals.css
mv app/globals.css src/index.css

# Remove next specific directories and files
rm -rf .next
rm -rf app
rm -f next-env.d.ts next.config.ts

echo "Refactor script setup complete"
