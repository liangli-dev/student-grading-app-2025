import { createRouter, createWebHistory } from 'vue-router';
import StudentByClass from '../views/StudentByClass.vue';
import Home from '../views/Home.vue';
import TeacherList from '../views/TeacherList.vue';
import MatiereList from '../views/MatiereList.vue';
import NoteManagement from '../components/NoteManagement.vue';

const routes = [
    { path: '/', name: 'Home', component :Home},
    { path: '/class/:id', component: StudentByClass},
    { path:'/teachers', component: TeacherList},
    { path: '/matieres', component: MatiereList},
    { path: '/notes', component: NoteManagement},
    { path: '/classes', component: StudentByClass}
];

export default createRouter({
    history: createWebHistory(),
    routes
});