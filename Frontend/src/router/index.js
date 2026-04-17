import { createRouter, createWebHistory } from 'vue-router'

import DashboardView from '../views/DashboardView.vue'
import AlumnosView from '../views/AlumnosView.vue'
import NotasView from '../views/NotasView.vue'
import ReporteView from '../views/ReporteView.vue'

const routes = [
  { path: '/', redirect: '/dashboard' },

  {
    path: '/dashboard',
    component: DashboardView,
    children: [
      { path: '', component: AlumnosView },
      { path: 'notas', component: NotasView },
      { path: 'reporte', component: ReporteView },
    ],
  },
]

export default createRouter({
  history: createWebHistory(),
  routes,
})
