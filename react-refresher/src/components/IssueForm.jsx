import {useState} from 'react'

function IssueForm({ onIssueCreated }) {
    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')
    const [status, setStatus] = useState('Open')

    async function handleSubmit(event) {
    event.preventDefault()

    const newIssue = {
        title,
        description: description || null,
        status
    }

    try {
        const response = await fetch(
            'http://localhost:5201/api/issues', 
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(newIssue)
            }
        )

        if (!response.ok) {
            throw new Error(`HTTP error: ${response.status}`)
        }

        const createdIssue = await response.json()

        onIssueCreated(createdIssue)

        setTitle('')
        setDescription('')
        setStatus('Open')
    } catch (error) {
        console.error('Could not create issue:', error)
    }
}

    return (
        <form onSubmit={handleSubmit}>
            <h2>Skapa ärende</h2>

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
                Skapa
            </button>
        </form>
    )
}

export default IssueForm