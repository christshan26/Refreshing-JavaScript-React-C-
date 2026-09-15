import { useEffect, useState } from 'react'
import IssueItem from './IssueItem'
import IssueForm from './IssueForm'

function IssueList() {
    const [issues, setIssues] = useState([])
    const [error, setError] = useState(null)
    const [issueToEdit, setIssueToEdit] = useState(null)

    useEffect(() => {
        async function loadIssues() {
        try {
                const response = await fetch('http://localhost:5201/api/issues')

                if (!response.ok) {
                throw new Error(`HTTP error: ${response.status}`)
                }

                const data = await response.json()

                setIssues(data)
            } catch (error) {
                console.error('Could not load issues:', error)
                setError('Kunde inte hämta ärenden.')
            }
        }

        loadIssues()
    }, [])

    function handleIssueCreated(createdIssue) {
        setIssues(currentIssues => [
            createdIssue,
            ...currentIssues
        ])
    }

    function handleIssueUpdated(updatedIssue) {
        setIssues(currentIssues => 
            currentIssues.map(issue =>
                issue.id === updatedIssue.id 
                ? updatedIssue 
                : issue
            )
        )

        setIssueToEdit(null)
    }

    return (
        <div>

            <IssueForm
            key={issueToEdit?.id ?? 'new'}
            issueToEdit={issueToEdit}
            onIssueCreated={handleIssueCreated}
            onIssueUpdated={handleIssueUpdated}
            />

            <h2>Ärenden</h2>

            {error && <p>{error}</p>}

            <ul>
                {issues.map(issue => (
                    <IssueItem
                        key={issue.id}
                        issue={issue}
                        onEdit={setIssueToEdit}
                    />
                ))}
            </ul>
        </div>
    )
}

export default IssueList