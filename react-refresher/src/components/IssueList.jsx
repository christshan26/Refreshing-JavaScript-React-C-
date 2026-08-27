import { useEffect, useState } from 'react'
import IssueItem from './IssueItem'

function IssueList() {
    const [issues, setIssues] = useState([])

    useEffect(() => {
        async function loadIssues() {
            const response = await fetch('http://localhost:5201/api/issues')
            const data = await response.json()

            setIssues(data)
        }

        loadIssues()
    }, [])

    return (
        <div>
            <h2>Ärenden</h2>

            <ul>
                {issues.map(issue => (
                    <IssueItem
                        key={issue.id}
                        issue={issue}
                    />
                ))}
            </ul>
        </div>
    )
}

export default IssueList