const API_URL = 'http://localhost:5166/api/classes';

export async function getProfesseurPrincipal(classId) {
  const res = await fetch(`${API_URL}/${classId}/principal-teacher`);
  if (!res.ok) {
    if (res.status === 404) return null;
    throw new Error(`HTTP error! status: ${res.status}`);
  }
  return await res.json();
}

export async function getAllClasses() {
  const res = await fetch(API_URL);
    if (!res.ok) {
          throw new Error(`HTTP error! Status: ${res.status}`);
      }
      return await res.json();
}