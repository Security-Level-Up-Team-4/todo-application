const Loader = () => {
     const circleCommonClasses = 'h-2.5 w-2.5 bg-current rounded-full';

     return (
    <section className='flex m-auto'>
         <section className={`${circleCommonClasses} mr-1 animate-bounce`}></section>
         <section className={`${circleCommonClasses} mr-1 animate-bounce-200`}></section>
         <section className={`${circleCommonClasses} animate-bounce-400`}></section>
    </section>
     );
};

export default Loader;