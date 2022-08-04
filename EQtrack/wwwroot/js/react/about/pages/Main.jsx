import AboutSect from '../components/AboutSect.jsx';

function Main() {


    return (<div>
        <div className="container-fluid back">
            <div className="row">
                <h1>About</h1>
            </div>
            <AboutSect src="/images/proTeam.jfif" text="Our dedicated team of professionals are here to facilitate your every professional tools need!" />
            <AboutSect src="/images/precision.jpg" text="Precision tools when and where your team needs them to be." />
            <AboutSect src="/images/tools.jpg" text="Having the right tool for the right job ensures our clients have the fastest access and most up-to-date professional line- up of tools available through our professional cataloguing and organization of each corporate tool inventory." />
            <AboutSect src="/images/repair.jpg" text="We save our clients millions of dollars and hundreds of man hours in tool overhead cost for lost and damaged tools." />
            <AboutSect src="/images/tech.jpg" text="EQTrack uses cutting edge technology to keep us on track for all our equipment needs!" />
        </div>
    </div>)
}

export default Main;