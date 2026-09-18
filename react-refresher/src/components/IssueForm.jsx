import {useState} from 'react'

function IssueForm({ 
    issueToEdit,
    categories,
    onIssueCreated,
    onIssueUpdated
}) {
    const [title, setTitle] = useState(issueToEdit?.title ?? '')
    const [description, setDescription] = useState(issueToEdit?.description ?? '')
    const [status, setStatus] = useState(issueToEdit?.status ?? 'Open')
    const [categoryId, setCategoryId] = useState(issueToEdit?.categoryId ?? '')

    async function handleSubmit(event) {
    event.preventDefault()

    const newIssue = {
        title,
        description: description || null,
        status,
        categoryId: categoryId === '' ? null : categoryId
    }

    const isEditing = issueToEdit !== null

    const url = isEditing
        ? `http://localhost:5201/api/issues/${issueToEdit.id}`
        : 'http://localhost:5201/api/issues'

    const method = isEditing ? 'PUT' : 'POST'

    try {
        const response = await fetch( url, {
            method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(newIssue)
        })

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`)
        }

        const savedIssue = await response.json()

        if (isEditing) {
            onIssueUpdated(savedIssue)
        } else {
            onIssueCreated(savedIssue)
        }

        setTitle('')
        setDescription('')
        setStatus('Open')
    } catch (error) {
        console.error('Could not create issue:', error)
    }
}

    return (
        <form onSubmit={handleSubmit}>
            <h2>
                {issueToEdit ? 'Redigera ärende' : 'Skapa ärende'}
            </h2>

            <div>
                <label htmlFor="title">Titel:</label>

                <input
                    id="title"
                    type="text"
                    value={title}
                    onChange={event => setTitle(event.target.value)}
                />
            </div>

            <div>
                <label htmlFor="description">Beskrivning:</label>

                <textarea
                    id="description"
                    value={description}
                    onChange={event => setDescription(event.target.value)}
                />
            </div>

            <div>
                <label htmlFor="category">Kategori</label>

                <select
                    id="category"
                    value={categoryId}
                    onChange={event => setCategoryId(event.target.value === '' ? '' : Number(event.target.value))}>
                        <option value="">Ingen kategori</option>

                        {categories.map(category => (
                            <option
                                key={category.id}
                                value={category.id}
                            >
                                {category.name}
                            </option>
                        ))}
                </select>
            </div>

            <div>
                <label htmlFor="status">Status:</label>

                <select
                    id="status"
                    value={status}
                    onChange={event => setStatus(event.target.value)}
                >
                    <option value="Open">Open</option>
                    <option value="In Progress">In Progress</option>
                    <option value="Done">Done</option>
                </select>
            </div>

            <button type="submit">
                {issueToEdit ? 'Spara ändringar' : 'Skapa'}
            </button>
        </form>
    )
}

export default IssueForm