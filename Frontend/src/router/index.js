import { createRouter, createWebHistory } from 'vue-router'
import CvEditor from '../views/CvEditor.vue'
import AdminPanel from '../views/AdminPanel.vue'

const routes = [
    { path: '/', component: CvEditor },
    { path: '/admin', component: AdminPanel },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router 
