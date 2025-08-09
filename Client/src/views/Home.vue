<template>
  <div>
    <div v-if="$route.path === '/'">
        <button @click="enterSystem"> Acèsse au Systeme </button>
    </div>
    <router-view />
  </div>
</template>

<script setup>
import { useRouter } from 'vue-router';

const router = useRouter();

const enterSystem = () => {
  router.push('/class/1');
}
</script>