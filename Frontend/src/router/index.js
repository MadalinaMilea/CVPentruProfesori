import { createRouter, createWebHistory } from 'vue-router'
import CvEditor from '../views/CvEditor.vue'

const routes = [
    { path: '/', component: CvEditor },
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router 
