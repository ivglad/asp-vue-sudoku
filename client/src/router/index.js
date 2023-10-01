import { createRouter, createWebHistory } from "vue-router";
import Sudoku from "@/views/Sudoku.vue";

const routes = [
  {
    path: "/",
    name: "Sudoku",
    component: Sudoku,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
