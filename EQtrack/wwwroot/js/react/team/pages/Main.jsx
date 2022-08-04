import Card from '../components/Card.jsx'

function Main() {

    var title = "Executive Director"

    return (<div>
        <div className="container-fluid backDrop">
            <div className="row rowPad">
                <h1 className="title">Our Team</h1>

                <h4>“Organization is the key to sanity.”</h4>
            </div>
            <div className="cards">
                <div className="row rowPad">
                    <h4>We keep you on your work TRACK;  EQTrack, so your company can focus on doing the job and not lose valuable time finding or repairing tools!</h4>
                </div>
                <div className="row rowPad">
                    <div className="col-md">
                        <Card title="Sean Mahoney" icon="/images/sean.jpg" comment={title} />
                    </div>
                    <div className="col-md">
                        <Card title="Mindy Lee" icon="/images/mindy2.0.jpg" comment={title} />
                    </div>
                    <div className="col-md">
                        <Card title="Mustafa Harvey" icon="/images/mustafa.jfif" comment={title} />
                    </div>
                    <div className="col-md">
                        <Card title="Isaac Marsden" icon="/images/isaac.jpg" comment={title} />
                    </div>
                </div>
            </div>
        </div>
    </div>)
}

export default Main;