<template>
    <li 
    @click="$emit('select', student)"
    :class="{ 'selected': isSelected }"
  >
    <span> ID {{student.id }} . {{ student.firstName }} {{ student.lastName }} - {{ student.gender }}</span>
    <div class="item-actions">
      <button @click.stop="$emit('edit', student)">Modifier</button>
      <button @click.stop="$emit('delete', student)">Supprimer</button>
    </div>
    </li>
</template>

<script>
export default {
    props :{
        student : Object
    }
}
</script>

<style scoped>
li {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px;
  border-bottom: 1px solid #eee;
  cursor: pointer;
}
li.selected {
  background-color: #f5f5f5;
}
.item-actions button {
  margin-left: 10px;
  padding: 5px 10px;
  background: #42b883;
  color: white;
  border: none;
  border-radius: 3px;
}
</style>