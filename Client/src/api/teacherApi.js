const API_URL = 'http://localhost:5166/api/teachers';

export async function getAllTeachers() {
  const res = await fetch(API_URL);
  return await res.json();
}

export async function getTeacherGroupedByMatiere() {
  const res = await fetch(`${API_URL}/by-matiere`);
  if (!res.ok) throw new Error(`HTTP error! status: ${res.status}`);
  return await res.json();
}

export async function addTeacher(teacherData) {
  const res = await fetch(API_URL, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({
        firstName: teacherData.firstName,
        lastName: teacherData.lastName,
        gender: teacherData.gender
      })
  });
  return await res.json();
}

export async function updateTeacher(teacherData) {
  const res = await fetch(`${API_URL}/${teacherData.id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(teacherData)
  });
const text = await res.text();
    try {
        return JSON.parse(text); 
    } catch {
        return { message: text }; 
    }
}

export async function deleteTeacher(id) {
  const res = await fetch(`${API_URL}/${id}`, {
    method: 'DELETE'
  });
  return await res.text();
}
