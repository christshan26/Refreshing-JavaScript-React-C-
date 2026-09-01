import {useState} from 'react'

function IssueForm() {
    const [title, setTitle] = useState('')
    const [description, setDescription] = useState('')
    const [status, setStatus] = useState('Open')

    return (
        <form>
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