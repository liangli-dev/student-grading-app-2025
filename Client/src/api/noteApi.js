const API_URL = 'http://localhost:5166/api/notes';

export async function getNoteByStudent(studentId) {
    const res = await fetch (`${API_URL}/by-student/${studentId}`);
    return await res.json();
}

export async function addNote(note) {
    const res = await fetch (`${API_URL}`, {
        method : 'POST',
        headers : {'Content-Type': 'application/json'},
        body : JSON.stringify(note)
    })
    const text = await res.text();
  if (!res.ok) {
    console.error('addNote error response:', text);
    throw new Error(text);
  }
  try { return JSON.parse(text); } catch { return { message: text }; }
}

export async function updateNote( id,note ) {
    const res = await fetch(`${API_URL}/${id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(note)
  });
    const text = await res.text();
  if(!res.ok)
    {   
        console.error('updateNote error response:', text);
        try {
            return JSON.parse(text); 
        } catch {
            return { message: text }; 
        }
    }
    try { return JSON.parse(text); } catch { return { message: text }; }
}

export async function deleteNote(id) {
    const res = await fetch(`${API_URL}/${id}`,{
        method :'DELETE',
    });
      if (!res.ok) {
        const error = await res.text(); 
        throw new Error(error || "Suppression échoué.");
        }
        try {
            return await res.json();
        } catch {
            return { success: true }; 
        }
}
