// Vue imports
//import { createRouter, createWebHistory } from 'vue-router'
import Vue from 'vue';
import Router from 'vue-router'
// my own imports
import Hello from '@/components/Hello'
import Covid19Cases from '@/components/Covid19Cases'
import SignUp from '@/components/SignUp'
import Login from '@/components/Login'
import AddCase from '@/components/AddCase'

import Swal from 'sweetalert2'

Vue.use(Router);

let router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      name: 'Hello',
      component: Hello
    },
    {
      path: '/covid19cases',
      name: 'Covid19Cases',
      component: Covid19Cases,
    },
    {
      path: '/signup',
      name: 'SignUp',
      component: SignUp
    },
    {
      path: '/login',
      name: 'Login',
      component: Login
    },
    {
      path: '/addcase',
      name: 'AddCase',
      component: AddCase
    },
  ]
})

router.beforeEach((to, from, next) => {
  const publicPages = ['/login', '/signup', '/', '/covid19cases'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('token');

  if (to.path == '/logout') {
    window.localStorage.removeItem("token");
    next('/login');
    router.go(0);
  }

  if (authRequired && !loggedIn) {
    next('/login');
    Swal.fire("This page is for authorized users only!")

  } else {
    next();
  }
});

export default router;
