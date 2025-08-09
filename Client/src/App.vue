<template>
  <div>
    <header class="global-header"> 
      <h1> Système de Notes </h1>
    </header>
    <main class="router-content">
        <router-view />
    </main>
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';

const $router = useRouter();

</script>

<style>

.global-header {
  background: white; 
  color: black;
  padding: 16px 20px;
  text-align: left;
  position: fixed;
  top :0;
  left: 0;
  width: 100%;
}

.global-header h1{
  margin: 0;
  font-size: 1.5rem;
  font-weight: 600;
}

.router-content {
  flex: 1;
  padding: 20px;
  max-width: 1200px;
  margin: 80 auto 0;
  width: 100%;
}
</style>
